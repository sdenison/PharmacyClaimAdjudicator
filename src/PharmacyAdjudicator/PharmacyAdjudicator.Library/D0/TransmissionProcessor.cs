using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NxBRE.InferenceEngine;
using NxBRE.InferenceEngine.IO;

namespace PharmacyAdjudicator.Library.D0
{
    public class TransmissionProcessor
    {
        public static Response.Transmission Process(Submitted.Transmission submittedTransmission, IRuleBaseAdapter rules = null)
        {
            Response.Transmission responseTransmission;

            switch (submittedTransmission.TransactionType)
            {
                case Submitted.Transmission.TransactionTypeEnum.Billing:
                    responseTransmission = ProcessBilling(submittedTransmission, rules);
                    break;
                case Submitted.Transmission.TransactionTypeEnum.Reversal:
                    responseTransmission = ProcessReversal(submittedTransmission);
                    break;
                case Submitted.Transmission.TransactionTypeEnum.Rebill:
                    responseTransmission = ProcessReversal(submittedTransmission);
                    break;
                default:
                    throw new Exception("Transaction Type unknown: Transaction Type = " + submittedTransmission.TransactionType.ToString());
            }
            return responseTransmission;
        }

        //Injecting rules for now until I get rules tied to plans
        public static Response.Transmission ProcessBilling(Submitted.Transmission submittedTransmission, IRuleBaseAdapter rules)
        {
            Response.Transmission response = new Response.Transmission();
            response.TransactionHeader = new Response.TransactionHeaderSegment(submittedTransmission.TransactionHeader);
            //lookup the person
            Core.Patient patient;
            try
            {
                patient = Core.Patient.GetByTransmissionCriteria(submittedTransmission.TransactionHeader.ProcessorControlNumber,
                    submittedTransmission.Insurance.CardholderId,
                    submittedTransmission.Insurance.GroupId,
                    submittedTransmission.Patient.DateOfBirth,
                    submittedTransmission.Patient.PatientLastName,
                    submittedTransmission.Patient.PatientGenderCode,
                    submittedTransmission.Insurance.PersonCode,
                    submittedTransmission.Insurance.PatientRelationshipCode);

                //Sets the Patient Response segment with data from the patient that was looked up
                response.Patient = new Response.PatientSegment();
                response.Patient.PatientFirstName = patient.FirstName;
                response.Patient.PatientLastName = patient.LastName;
                response.Patient.DateOfBirth = patient.DateOfBirth;
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is DataNotFoundException)
                {
                    //return patient not found response
                    response.Message = new Response.MessageSegment();
                    response.Message.Message = "No patient found";
                    return response;
                }
                else
                    throw ex;
            }


            //lookup insurance information
                //Only the Bin and Processor Control Number are available to specify what plan is used.

            //Need to figure out if patient is covered
            bool patientCovered = true;
            if (patientCovered)
            { 

                //lookup service provider
                //lookup prescriber information

                //if anything is hinkey in there send response message segment rejecting transmission
                foreach (var claim in submittedTransmission.Claims)
                {
                    //Task<Core.TransactionList> patientTransactions = Core.TransactionList

                    var drug = Core.Drug.GetByNdc(claim.Claim.ProductServiceId);
                    //var transaction = new Core.Transaction(drug, patient, serviceProvider, prescriber, claim);
                    var transaction = new Core.Transaction(drug, patient, claim); //serviceProvider, prescriber, claim);

                    transaction.IngredientCostSubmitted = claim.Pricing.IngredientCostSubmitted.Value;

                    transaction.Save();

                    //Will modify the transaction during processing
                    Core.TransactionProcessor.Process(transaction, rules);
                    var responseClaim = new Response.ClaimBilling(transaction);
                    if (response.Claims == null)
                        response.Claims = new List<Response.ClaimBilling>();
                    response.Claims.Add(responseClaim);


                    //look up the claim
                    //if it already exists then make new response status segment for claim rejecting claim
                    //Combine insurance information (inference engine value) with drug, person and prescriber to get covered status
                    //if covered status passed then get pricing information with inference enginea
                    if (claim.PriorAuthorization != null)
                    {

                    }
                    //Lookup Prior Auth and mark it as used
                    //Core.Transaction transaction = Core.Transaction.GetBySubmittedClaim(claim);
                    //if (transaction.IsNew == true)
                    //{
                    //    transaction.Save();
                    //    //transaction is new and we need to keep processing the response
                    //}
                    //else
                    //{
                    //    //transaction is a repeat and we need to return 




                    //}
                }
            }

            return response;
        }

        //private Core.Patient GetPatient(Submitted.Transmission submittedTransmission)
        //{

        //}

        private Response.ClaimBilling ProcessClaim(Submitted.ClaimBilling submittedClaim)
        {
            Response.ClaimBilling response = new Response.ClaimBilling();

            //if (submittedClaim.

            return response;
        }

        public static Response.Transmission ProcessReversal(Submitted.Transmission submittedTransmission)
        {
            //Test reversals send as little as Date of Service, Service Provider ID, Prescription Number and Product Service ID to identify the claim
            return new Response.Transmission();
        }

        public static Response.Transmission ProcessRebill(Submitted.Transmission submitttedTransmission)
        {
            return new Response.Transmission();
        }


    }
}
