using GeneralTillApp.Data;
using GeneralTillApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace GeneralTillApp.Managers
{
    public class PaymentManager
    {

        #region Properties

        public NavigationManager _navManager { get; set; }
        public ApplicationDbContext _context { get; set; }
        public int ElapsedTime { get; set; }
        public int DesiredTimeout { get; set; } = 3;
        public int CounterInterval { get; set; } = 1;
        public Timer Counter { get; set; }
        public bool PaymentTimeoutBool { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public bool PaymentRunning { get; set; }

        public delegate void CounterIncrementEventHandler(object source, EventArgs args);
        public event CounterIncrementEventHandler CounterIncrement;

        public delegate void PaymentTimeoutEventHandler(object source, EventArgs args);
        public event PaymentTimeoutEventHandler PaymentTimeout;

        #endregion

        #region Constructor


        // Not sure if the nav manager will be needed, but its here in case 
        public PaymentManager(NavigationManager navigationManager, ApplicationDbContext context)
        {
            _context = context;
            _navManager = navigationManager;
        }

        #endregion

        #region Methods


        public async Task<Transaction> MakePayment(Transaction transaction, PaymentTypeEnum paymentType, IEnumerable<CartItem> cartItems)
        {
            // Always sets the payment success to false to make sure there are no false approvals
            PaymentStatus = PaymentStatusEnum.None;

            while (PaymentRunning)
            {
                BeginCounter();
                transaction.CartItems = cartItems;

                if (paymentType == PaymentTypeEnum.Park)
                {
                    PaymentStatus = PaymentStatusEnum.Failure;
                    // return await SaveTransaction(transaction);
                }

                if (paymentType == PaymentTypeEnum.AR)
                {
                    transaction.Customer.ARBalance -= transaction.Total;
                    _context.Customers.Update(transaction.Customer);
                    await _context.SaveChangesAsync();
                    return await SaveTransaction(transaction);
                }

                if (transaction.AmountPaid > transaction.Total)
                {
                    switch (paymentType)
                    {
                        case PaymentTypeEnum.Cash:
                            transaction.AmountOwed = Math.Abs(transaction.AmountPaid - transaction.Total);
                            PaymentStatus = PaymentStatusEnum.Success;
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Credit:
                            PaymentStatus = await ProcessCredit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Debit:
                            PaymentStatus = await ProcessDebit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Cheque:
                            return transaction;
                        default:
                            // If we hit here there was an error, time out payment
                            return transaction;
                    }

                }

                else if (transaction.AmountPaid == transaction.Total)
                {
                    switch (paymentType)
                    {
                        case PaymentTypeEnum.Cash:
                            PaymentStatus = PaymentStatusEnum.Success;
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Credit:
                            PaymentStatus = await ProcessCredit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Debit:
                            PaymentStatus = await ProcessDebit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Cheque:
                            return transaction;
                        default:
                            // If we hit here there was an error, time out payment
                            return transaction;
                    }

                }

                else if (transaction.AmountPaid < transaction.Total)
                {
                    switch (paymentType)
                    {
                        case PaymentTypeEnum.Cash:
                            return transaction;
                        case PaymentTypeEnum.Credit:
                            PaymentStatus = await ProcessCredit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Debit:
                            PaymentStatus = await ProcessDebit();
                            return await SaveTransaction(transaction);
                        case PaymentTypeEnum.Cheque:
                            return transaction;
                        default:
                            // If we hit here there was an error, time out payment
                            return transaction;
                    }
                }

                else
                    return transaction;
            }
            return transaction;
        }

        // Save current transaction to the db along with cart items 
        private async Task<Transaction> SaveTransaction(Transaction transaction)
        {
            transaction.PurchaseDate = DateTime.Now;
            transaction.TransactionNumber = GenerateTransactionNumber(transaction.PurchaseDate);

            foreach (var cartItem in transaction.CartItems)
            {
                cartItem.TransactionNumber = transaction.TransactionNumber;
                _context.CartItems.Add(cartItem);
            }

            if (transaction.Customer == null)
                transaction.Customer = _context.Customers.Where(c => c.FirstName == "None").FirstOrDefault();

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            PaymentRunning = false;
            return transaction;
        }

        // Generates the transaction number based on the current date time 
        private string GenerateTransactionNumber(DateTime dateTime)
        {
            var transNumber = $"{dateTime.Day}{dateTime.Month}{dateTime.Year}{dateTime.Hour}{dateTime.Minute}{dateTime.Second}";
            return transNumber;
        }

        private async Task<PaymentStatusEnum> ProcessDebit()
        {
            await Task.Delay(5000);
            return PaymentStatusEnum.Success;
        }

        private async Task<PaymentStatusEnum> ProcessCredit()
        {
            await Task.Delay(5000);
            return PaymentStatusEnum.Success;
        }

        // Starts counter
        private void BeginCounter()
        {
            Counter = new Timer(CounterInterval);
            Counter.Start();
            Counter.Elapsed += new ElapsedEventHandler(OnCounterIncrement);
        }

        // Send the Elapsed time to the Cash register component 
        protected virtual void OnCounterIncrement(object source, ElapsedEventArgs e)
        {
            ElapsedTime++;

            if (ElapsedTime <= DesiredTimeout)
            {
                CounterIncrement(ElapsedTime, EventArgs.Empty);
            }
            else
            {
                PaymentTimeoutBool = true;
                PaymentTimeout(PaymentTimeoutBool, EventArgs.Empty);
                Counter.Stop();
                Counter.Close();
                PaymentStatus = PaymentStatusEnum.Failure;
                // TODO :: Add messaging feature to inform that there has been a timeout
                PaymentRunning = false;
            }
        }
        #endregion
    }
}
