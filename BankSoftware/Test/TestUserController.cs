using BankSoftware.Controllers;
using BankSoftware.Utilities;
using BankSoftwareManager.IManager;
using BankSoftwareManager.Manager;
using BankSoftwareModels;
using NSubstitute;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankSoftwareDataAccess.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSoftware.Test
{
    [TestClass]
    public class TestUserController
    {
        private readonly IAccountTypeRepository _accountTypeRepository = Substitute.For<IAccountTypeRepository>();
        private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
        private readonly IPaymentFrequencyRepository _paymentFrequencyRepository = Substitute.For<IPaymentFrequencyRepository>();
        private readonly IPaymentDetailRepository _paymentDetailRepository = Substitute.For<IPaymentDetailRepository>();
        private readonly IBankRepository _bankRepository = Substitute.For<IBankRepository>();
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly IAccountResourceTypeRepository _accountResourceTypeRepository = Substitute.For<IAccountResourceTypeRepository>();
        private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
        private readonly IRecurringPaymentRepository _recurringPaymentRepository = Substitute.For<IRecurringPaymentRepository>();
        private readonly IResourceRepository _resourceRepository = Substitute.For<IResourceRepository>();

        AccountTypeManager accountTypeManager;
        AccountManager accountManager;
        UserManager userManager;
        AccountResourceTypeManager accountResourceTypeManager;
        ReccuringPaymentManager reccuringPaymentManager;
        ResourceManager resourceManager;
        BankManager bankManager;



        public TestUserController()
        {
            accountTypeManager = new AccountTypeManager(_accountTypeRepository);
            accountManager = new AccountManager(_accountRepository, _userRepository, _bankRepository, _accountTypeRepository);
            userManager = new UserManager(_userRepository);
            accountResourceTypeManager = new AccountResourceTypeManager(_accountResourceTypeRepository);
            reccuringPaymentManager = new ReccuringPaymentManager(_accountRepository, _recurringPaymentRepository, _paymentFrequencyRepository, _paymentDetailRepository);
            resourceManager = new ResourceManager(_resourceRepository);
            bankManager = new BankManager(_bankRepository);

        }
        
        [TestMethod]
        public void TestAccountType()
        {
            Guid accountTypeId = Guid.Parse("8871598D-2A6E-46A7-80D9-44C17B316EAE");
            var accountType = new AccountType { AccountTypePk = Guid.Parse("8871598D-2A6E-46A7-80D9-44C17B316EAE"), Name = "Type 1",Description="Description 1"};
            _accountTypeRepository.GetAccountTypeById(accountTypeId).Returns(accountType);

            var result = accountTypeManager.GetAccountTypeById(accountTypeId);

            Assert.IsInstanceOfType(result, typeof(AccountType));
        }
        [TestMethod]
        public void TestGetAllAccountType()
        {
            Guid accountTypeId = Guid.Parse("8871598D-2A6E-46A7-80D9-44C17B316EAE");
            var accountType = new AccountType { AccountTypePk = Guid.Parse("8871598D-2A6E-46A7-80D9-44C17B316EAE"), Name = "Type 1", Description = "Description 1" };
            var accountType1 = new AccountType { AccountTypePk = Guid.Parse("7871598D-2A6E-46A7-80D9-44C17B316EAA"), Name = "Type 2", Description = "Description 2" };
            List<AccountType> list = new List<AccountType>();
            list.Add(accountType);
            list.Add(accountType1);
            _accountTypeRepository.GetAllAccountType().Returns(list);

            var result = accountTypeManager.GetAllAccountType();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestSaveUpdateAccountType()
        {
            Guid accountTypeId = Guid.NewGuid();
            var accountType = new AccountType { AccountTypePk = accountTypeId, Name = "Type 1", Description = "Description 1" };
            _accountTypeRepository.SaveUpdateAccountType(accountType).Returns(accountTypeId);

            var result = accountTypeManager.SaveUpdateAccountType(accountType);

            Assert.AreEqual(accountTypeId, result);
        }

        [TestMethod]
        public void TestGetAccountById()
        {
            Guid accountTypeId = Guid.NewGuid();
            var account = new Account
            {
                AccountPk = accountTypeId,
                AccountName = "Account 1",
                AccountNumber = "8989898",
                UserFk = Guid.NewGuid(),
                BankFk = Guid.NewGuid(),
                Balance = 1000,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now,
                IsActive = true,
                SecurityPIN = 1133,
                IsDefault = true,
                IsDeleted = false
            };
            _accountRepository.GetAccountById(accountTypeId).Returns(account);

            var result = accountManager.GetAccountById(accountTypeId);

            Assert.IsInstanceOfType(result, typeof(Account));
        }


        [TestMethod]
        public void TestSaveUpdateAccount()
        {
            Guid accountTypeId = Guid.NewGuid();
            var account = new Account
            {
                AccountPk = accountTypeId,
                AccountName = "Account 1",
                AccountNumber = "8989898",
                UserFk = Guid.NewGuid(),
                BankFk = Guid.NewGuid(),
                Balance = 1000,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now,
                IsActive = true,
                SecurityPIN = 1133,
                IsDefault = true,
                IsDeleted = false
            };
            _accountRepository.SaveUpdateAccount(account).Returns(accountTypeId);

            var result = accountManager.SaveUpdateAccount(account);
            Assert.AreEqual(accountTypeId, result);
        }

        [TestMethod]
        public void TestDeleteAccount()
        {
            Guid accountTypeId = Guid.NewGuid();
            var account = new Account
            {
                AccountPk = accountTypeId,
                AccountName = "Account 1",
                AccountNumber = "8989898",
                UserFk = Guid.NewGuid(),
                BankFk = Guid.NewGuid(),
                Balance = 1000,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now,
                IsActive = true,
                SecurityPIN = 1133,
                IsDefault = true,
                IsDeleted = false
            };
            _accountRepository.DeleteAccount(account).Returns(accountTypeId);

            var result = accountManager.DeleteAccount(account);

            Assert.AreEqual(accountTypeId, result);
        }

        [TestMethod]
        public void TestGetBankById()
        {
            Guid bankId = Guid.NewGuid();
            var bank = new Bank
            {
                BankPk = bankId,
                BankName = "Bank 1"
            };
            _bankRepository.GetBankById(bankId).Returns(bank);

            var result = bankManager.GetBankById(bankId);

            Assert.IsInstanceOfType(result, typeof(Bank));
        }


        [TestMethod]
        public void TestSaveUpdateBank()
        {
            Guid bankId = Guid.NewGuid();
            var bank = new Bank
            {
                BankPk = bankId,
                BankName = "Bank 1"
            };
            _bankRepository.SaveUpdateBank(bank).Returns(bankId);

            var result = bankManager.SaveUpdateBank(bank);
            Assert.AreEqual(bankId, result);
        }

        [TestMethod]
        public void TestGetAllBanks()
        {
            Guid bankId = Guid.NewGuid();
            var bank = new Bank
            {
                BankPk = bankId,
                BankName = "Bank 1"
            };
            List<Bank> list = new List<Bank>();
            list.Add(bank);
            _bankRepository.GetAllBanks().Returns(list);

            var result = bankManager.GetAllBanks();

            Assert.IsInstanceOfType(result, typeof(List<Bank>));
        }

        [TestMethod]
        public void TestGetRecurringPaymentById()
        {
            Guid paymentId = Guid.NewGuid();
            var payment = new RecurringPayment
            {
                RecurringPaymentPk = paymentId,
                Title = "Bank 1",
                Description = "Description 1",
                FrequencyFk = Guid.NewGuid(),
                NumberOfPayment = 2,
                CustomerAccountFk = Guid.NewGuid(),
                StartDate = DateTime.Now,
                ReceivableAccountFk = Guid.NewGuid(),
                PaymentAmount = 500,
                CreateDate = DateTime.Now
            };
            _recurringPaymentRepository.GetRecurringPaymentById(paymentId).Returns(payment);

            var result = reccuringPaymentManager.GetRecurringPaymentById(paymentId);
            Assert.IsInstanceOfType(result, typeof(RecurringPayment));
        }

        [TestMethod]
        public void TestSaveUpdatePaymentFrequency()
        {
            Guid paymentId = Guid.NewGuid();
            var payment = new PaymentFrequency
            {
                FrequencyPk = paymentId,
                Name = "PaymentFrequency 1"
            };
            _paymentFrequencyRepository.SaveUpdatePaymentFrequency(payment).Returns(paymentId);

            var result = reccuringPaymentManager.SaveUpdatePaymentFrequency(payment);
            Assert.AreEqual(paymentId, result);
        }

        [TestMethod]
        public void TestGetAllPaymentDetail()
        {
            Guid accountTypeId = Guid.NewGuid();
            var detail = new PaymentDetail
            {
                PaymentPk = accountTypeId,
                Description = "Account 1",
                RecurringPaymentFk = Guid.NewGuid(),
                PaymentAmount = 1000,
                PaymentDate = DateTime.Now
            };
            List<PaymentDetail> list = new List<PaymentDetail>();
            list.Add(detail);
            _paymentDetailRepository.GetAllPaymentDetail().Returns(list);

            var result = _paymentDetailRepository.GetAllPaymentDetail();

            Assert.IsInstanceOfType(result, typeof(List<PaymentDetail>));
        }

        [TestMethod]
        public void TestSaveUpdatePaymentDetail()
        {
            Guid accountTypeId = Guid.NewGuid();
            var detail = new PaymentDetail
            {
                PaymentPk = accountTypeId,
                Description = "Account 1",
                RecurringPaymentFk = Guid.NewGuid(),
                PaymentAmount = 1000,
                PaymentDate = DateTime.Now
            };
            List<PaymentDetail> list = new List<PaymentDetail>();
            list.Add(detail);
            _paymentDetailRepository.SaveUpdatePaymentDetail(detail).Returns(accountTypeId);

            var result = _paymentDetailRepository.SaveUpdatePaymentDetail(detail);

            Assert.IsInstanceOfType(result, typeof(Guid));
        }

        [TestMethod]
        public void TestSaveUpdateResource()
        {
            Guid resourceId = Guid.NewGuid();
            var resource = new Resource
            {
                ResourcePk = resourceId,
                CardNumber = "4545471",
                ResourceTypeFk = Guid.NewGuid(),
                IsActive = true,
                CheckbookNumber = "34344534",
                AccountFk = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                CreateDate = DateTime.Now,
                ModDate= DateTime.Now,
                CardPIN =2222


            };
            List<Resource> list = new List<Resource>();
            list.Add(resource);
            _resourceRepository.SaveUpdateResource(resource).Returns(resourceId);

            var result = _resourceRepository.SaveUpdateResource(resource);

            Assert.IsInstanceOfType(result, typeof(Guid));
        }

        [TestMethod]
        public void TestGetAllResource()
        {
            Guid resourceId = Guid.NewGuid();
            var resource = new Resource
            {
                ResourcePk = resourceId,
                CardNumber = "4545471",
                ResourceTypeFk = Guid.NewGuid(),
                IsActive = true,
                CheckbookNumber = "34344534",
                AccountFk = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now,
                CardPIN = 2222


            };
            List<Resource> list = new List<Resource>();
            list.Add(resource);
            _resourceRepository.GetAllResource().Returns(list);

            var result = _resourceRepository.GetAllResource();

            Assert.IsInstanceOfType(result, typeof(List<Resource>));
        }

        [TestMethod]
        public void TestGetResourceById()
        {
            Guid resourceId = Guid.NewGuid();
            var resource = new Resource
            {
                ResourcePk = resourceId,
                CardNumber = "4545471",
                ResourceTypeFk = Guid.NewGuid(),
                IsActive = true,
                CheckbookNumber = "34344534",
                AccountFk = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now,
                CardPIN = 2222


            };
            List<Resource> list = new List<Resource>();
            list.Add(resource);
            _resourceRepository.GetResourceById(resourceId).Returns(resource);

            var result = _resourceRepository.GetResourceById(resourceId);

            Assert.IsInstanceOfType(result, typeof(Resource));
        }

        [TestMethod]
        public void TestLoginUser()
        {
            string email = "abc@gmail.com";
            string password = "232323";

            ResponeModel<User> response = new ResponeModel<User>();
            _userRepository.LoginUser(email, password).Returns(response);

            var result = _userRepository.LoginUser(email, password);

            Assert.IsInstanceOfType(result, typeof(ResponeModel<User>));
        }

        [TestMethod]
        public void TestGetAllUser()
        {
            Guid userId = Guid.NewGuid();
            var user = new User
            {
                UserPk = userId,
                Name = "User",
                Secret = "Secret",
                ResetPasswordId = Guid.NewGuid(),
                Email = "email@gmail.com",
                Password = "3333",
                DOB = DateTime.Now,
                NIC = "34343434",
                Gender = "Male"


            };
            List<User> list = new List<User>();
            list.Add(user);
            _userRepository.GetAllUser().Returns(list);

            var result = _userRepository.GetAllUser();

            Assert.IsInstanceOfType(result, typeof(List<User>));
        }

        [TestMethod]
        public void TestGetUserById()
        {
            Guid userId = Guid.NewGuid();
            var user = new User
            {
                UserPk = userId,
                Name = "User",
                Secret = "Secret",
                ResetPasswordId = Guid.NewGuid(),
                Email = "email@gmail.com",
                Password = "3333",
                DOB = DateTime.Now,
                NIC = "34343434",
                Gender = "Male"


            };
            
            _userRepository.GetUserById(userId).Returns(user);

            var result = _userRepository.GetUserById(userId);

            Assert.IsInstanceOfType(result, typeof(User));
        }
    }

    
}