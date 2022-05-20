using Autofac.Core;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class HouseKeeperServiceTests
    {
        private HouseKeeperService _service;

        public Mock<IStatementGenerator> _statementGenerator { get; private set; }
        public Mock<IEmailSender> _emailSender { get; private set; }
        public Mock<IXtraMessageBox> _messageBox { get; private set; }
        private string _StatementFileName ;

        DateTime statementDate = new DateTime(2017, 1, 1);
       private  Housekeeper _housekeeper;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());
            _statementGenerator = new Mock<IStatementGenerator>();
            _StatementFileName = "fileName";
            _statementGenerator.
                Setup(sg => sg.SaveStatement(1, "b", statementDate)).
                Returns(() => _StatementFileName);
            _emailSender = new Mock<IEmailSender>();
            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();
            _messageBox = new Mock<IXtraMessageBox>();
            _service = new HouseKeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {

            _service.SendStatementEmails(statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(1, "b", statementDate));
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = null;
            _service.SendStatementEmails(statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(1, "b", statementDate),Times.Never);
        }
        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = "  ";
            _service.SendStatementEmails(statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(1, "b", statementDate), Times.Never);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            
            _service.SendStatementEmails(statementDate);
            VerifyEmailSent();    
        }
        [Test]
        public void SendStatementEmails_FileNameIsNull_ShouldNotEmailTheStatement()
        {
            _StatementFileName = null;
            _service.SendStatementEmails(statementDate);
            //_emailSender.Verify(es => es.EmailFile(_housekeeper.Email,
            //    _housekeeper.StatementEmailBody,
            //    _StatementFileName, It.IsAny<string>()),Times.Never);

            // to make test cleaner you can not specify emailafaile arguments just put any string because we don't need specific value 
            //_emailSender.Verify(es => es.EmailFile(It.IsAny<string>(),
            //   It.IsAny<string>(),
            //   It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            // make readable tests with less lines
            VerifyEmailNotSent();

        }
        [Test]
        public void SendStatementEmails_FileNameIsEmpty_ShouldNotEmailTheStatement()
        {
            _StatementFileName = "";
            _service.SendStatementEmails(statementDate);
            VerifyEmailNotSent();

        }
        [Test]
        public void SendStatementEmails_FileNameIsWhiteSpace_ShouldNotEmailTheStatement()
        {
            _StatementFileName = " ";
            _service.SendStatementEmails(statementDate);
            VerifyEmailNotSent();
            
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _service.SendStatementEmails(statementDate);
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _StatementFileName, It.IsAny<string>()));
        }
        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
