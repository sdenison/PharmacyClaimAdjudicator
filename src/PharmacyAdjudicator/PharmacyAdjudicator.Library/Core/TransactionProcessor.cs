using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using NxBRE.InferenceEngine;
using NxBRE.InferenceEngine.IO;

namespace PharmacyAdjudicator.Library.Core
{
    public class TransactionProcessor
    {
        private TransactionProcessor()
        {
        }

        /// <summary>
        /// Processes the transaction
        /// </summary>
        /// <param name="transaction">Incoming transaction to process</param>
        /// <param name="rules">Rules to apply to the transaction</param>
        /// <returns>A transformation of the transaction according to the rules applied</returns>
        public static Transaction Process(Transaction transaction, IRuleBaseAdapter rules, IRuleBaseAdapter otherRules = null)
        {
            var binder = new TransactionProcessorBinder(transaction);
            var ie = new IEImpl(binder);

            Hashtable transactions = new Hashtable();
            transactions.Add("TRANSACTION", transaction);

            //Testing if multiple rule bases can be loaded
            if (otherRules != null)
            {
                foreach (var implication in otherRules.Implications)
                    rules.Implications.Add(implication);
            }
            ie.LoadRuleBase(rules);


            ie.Process(transactions);
            return transaction;
        }
    }
}
