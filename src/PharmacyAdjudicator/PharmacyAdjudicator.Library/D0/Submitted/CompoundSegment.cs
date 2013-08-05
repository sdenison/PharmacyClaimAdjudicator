using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class CompoundSegment
    {
        /// <summary>
        /// Segment Identification
        /// </summary>
        /// <remarks>
        /// <para>>NCPDP 111-AM</para>
        /// <para>Identifies the segment in the request and/or response</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Compound Dosage Form Description Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 450-EF</para>
        /// <para>Dosage form of the complete compound mixture.</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("450-EF")]
        public string CompoundDosageFormDescriptionCode { get; set; }

        /// <summary>
        /// Compound Dispensing Unit Form Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 451-EG</para>
        /// <para>NCPDP standard product billing codes.</para>
        /// </remarks>
        [Required]
        [MaxLength(1)]
        [NcpdpFieldAttribute("451-EG")]
        public string CompoundDispensingUnitFormIndicator { get; set; }

        /// <summary>
        /// Compound Ingredient Component Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 447-EC</para>
        /// <para>
        /// Count of compound product IDs (both active and inactive) in the 
        /// compound mixture submitted.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("447-EC")]
        public int CompoundIngredientComponentCount { get; set; }

        /// <summary>
        /// List holding the compound ingredients
        /// </summary>
        public List<CompoundIngredient> CompoundIngredients { get; set; }

        /// <summary>
        /// Compound Ingredient Modifier Count Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 362-2G</para>
        /// <para>
        /// Code indicating the number of Compound Ingredient Modifier Code 
        /// (363-2H).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("362-2G")] 
        public int CompoundIngredientModifierCountCode { get; set; }

        /// <summary>
        /// Compound Ingredient Modifier Code List
        /// </summary>
        /// <remarks
        /// <para>NCPDP 363-2H</para> 
        /// <para>
        /// Identifies special circumstances related to the dispensing/payment 
        /// of the product as identified in the Compound Product ID (489-TE).
        /// </para>
        /// </remarks>
        public List<string> CompoundIngredientModiferCodes { get; set; }

        public CompoundSegment(string[] fields)
        {
            CompoundIngredient currentCompound = null;
            foreach (string field in fields)
            {
                //Skips blank fields
                if (string.IsNullOrEmpty(field))
                    continue;
                string ncpdpField = field.Substring(0, 2).ToUpper();
                string ncpdpFieldValue = field.Substring(2).Trim();
                switch (ncpdpField)
                {
                    case "AM":
                        if (string.IsNullOrEmpty(this.SegmentIdentification) == false)
                            throw new InvalidIncomingLineException("Segment Identification already set.  Line is probably missing a segment separator.  " + fields.ToString());
                        this.SegmentIdentification = ncpdpFieldValue;
                        break;
                    case "EF":
                        this.CompoundDosageFormDescriptionCode = ncpdpFieldValue;
                        break;
                    case "EG":
                        this.CompoundDispensingUnitFormIndicator = ncpdpFieldValue;
                        break;
                    case "EC":
                        this.CompoundIngredientComponentCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "RE":
                        if (this.CompoundIngredients == null)
                            this.CompoundIngredients = new List<CompoundIngredient>();
                        currentCompound = new CompoundIngredient();
                        currentCompound.CompoundProductIdQualifier = ncpdpFieldValue;
                        this.CompoundIngredients.Add(currentCompound);
                        break;
                    case "TE":
                        currentCompound.CompoundProductId = ncpdpFieldValue;
                        break;
                    case "ED":
                        currentCompound.CompoundIngredientQuantity = (decimal)int.Parse(ncpdpFieldValue) / 1000;
                        break;
                    case "EE":
                        currentCompound.CompoundIngredientDrugCost = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "UE":
                        currentCompound.CompoundIngredientBasisOfCostDetermination = ncpdpFieldValue;
                        break;
                    case "2G":
                        this.CompoundIngredientModifierCountCode = int.Parse(ncpdpFieldValue);
                        break;
                    case "2H":
                        if (this.CompoundIngredientModiferCodes == null)
                            this.CompoundIngredientModiferCodes = new List<string>();
                        this.CompoundIngredientModiferCodes.Add(ncpdpFieldValue);
                        break;
                    default:
                        break;
                }
            }
            if (this.CompoundIngredientComponentCount != CompoundIngredients.Count)
                throw new InvalidIncomingLineException("Compound Ingredient Component Count does not equal number of Compound Ingredients. line = " + fields.ToString());
            //if (this.CompoundIngredientModifierCountCode != this.CompoundIngredientModiferCodes.Count)
            //    throw new InvalidIncomingLineException("Compound Ingredient Modifier Code Count does not equal number of Compound Ingredient Modifier Codes. line = " + fields.ToString());
        }

        public class CompoundIngredient
        {
            /// <summary>
            /// Compound Product ID Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 488-RE</para>
            /// <para>
            /// Code qualifying the type of product dispensed.
            /// </para>
            /// </remarks>
            [Required]
            [NcpdpFieldAttribute("488-RE")]
            public string CompoundProductIdQualifier { get; set; }

            /// <summary>
            /// Comound Product ID 
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 489-TE</para>
            /// <para>
            /// Product Identification of an ingredient used in a compound.
            /// </para>
            /// </remarks>
            [Required]
            [NcpdpFieldAttribute("489-TE")]
            public string CompoundProductId { get; set; }

            /// <summary>
            /// Compound Ingredient Quantity
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 448-ED</para>
            /// <para>
            /// Amount expressed in metric decimal units of the product included 
            /// in the compound mixture.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("448-ED")]
            public decimal CompoundIngredientQuantity { get; set; }

            /// <summary>
            /// Compound Ingredient Drug Cost
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 449-EE</para> 
            /// <para>
            /// Ingredient cost for the metric decimal quantity of the product 
            /// included in the compound mixture indicated in ‘Compound 
            /// Ingredient Quantity’ (Field 448-ED).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("449-EE")]
            public decimal? CompoundIngredientDrugCost { get; set; }

            /// <summary>
            /// Compound Ingredient Basis of Cost Determination
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 490-UE</para>
            /// <para>
            /// Code indicating the method by which the drug cost of an 
            /// ingredient used in a compound was calculated.
            /// </para>
            /// </remarks>
            public string CompoundIngredientBasisOfCostDetermination { get; set; }
        }

    }
}
