using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TestNinja.Mocking;
using UnitTestsCourse.Mocking;
using Xunit;

namespace UnitTestsCourseTests.Mocking
{
    public class HousekeeperServiceTests
    {
        private readonly Housekeeper _housekeeper;
        private readonly Mock<IStatementService> _statementService;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<IXtraMessageBox> _messageBox;
        private readonly Mock<IHousekeeperRepository> _mockRepository;
        private readonly HousekeeperService _housekeeperService;
        private readonly DateTime _statementDate = DateTime.Now;
        private string _fileName;


        public HousekeeperServiceTests()
        {
            _housekeeper = new Housekeeper() { Email = "housekeeper@test.com", FullName = "Test complete", Id = 1, StatementEmailBody = "Email" };

            _statementService = new Mock<IStatementService>();
            _emailService = new Mock<IEmailService>();
            _messageBox = new Mock<IXtraMessageBox>();
            _mockRepository = new Mock<IHousekeeperRepository>();

            _fileName = "filename";

            _mockRepository.Setup(rep => rep.GetHousekeepers()).Returns(new List<Housekeeper>() { _housekeeper }.AsQueryable());
            _statementService
                .Setup(service => service.SaveStatement(_housekeeper.Id, _housekeeper.FullName, _statementDate))
                .Returns(_fileName);

            _housekeeperService = new HousekeeperService(
                _mockRepository.Object,
                _statementService.Object,
                _emailService.Object,
                _messageBox.Object
            );
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyStatementSent();
        }

        [Fact]
        public void SendStatementEmails_WhenHouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = null;

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyStatementNeverSent();
        }

        [Fact]
        public void SendStatementEmails_WhenHouseKeeperEmailIsWhitespace_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = " ";

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyStatementNeverSent();
        }

        [Fact]
        public void SendStatementEmails_WhenHouseKeeperEmailIsEmpty_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = "";

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyStatementNeverSent();
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_ShouldEmailTheStatement()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Fact]
        public void SendStatementEmails_WhenStatementFileNameIsNull_ShouldNotSendEmail()
        {
            _fileName = null;

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Fact]
        public void SendStatementEmails_WhenStatementFileNameIsEmpty_ShouldNotSendEmail()
        {
            _fileName = "";

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Fact]
        public void SendStatementEmails_WhenStatementFileNameIsWhitespace_ShouldNotSendEmail()
        {
            _fileName = " ";

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Fact]
        public void SendStatementEmails_WhenEmailSendingFails_ShouldCallMessageBox()
        {
            _emailService
                .Setup(service => service.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            _housekeeperService.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
            => _emailService.Verify(service => service.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _fileName, It.IsAny<string>()), Times.Never);


        private void VerifyEmailSent()
            => _emailService.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _fileName, It.IsAny<string>()));


        private void VerifyStatementNeverSent()
            => _statementService.Verify(service => service.SaveStatement(_housekeeper.Id, _housekeeper.FullName, _statementDate), Times.Never);


        private void VerifyStatementSent()
            => _statementService.Verify(service => service.SaveStatement(_housekeeper.Id, _housekeeper.FullName, _statementDate));

    }
}