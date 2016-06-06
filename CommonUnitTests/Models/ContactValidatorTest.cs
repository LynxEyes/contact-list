using Common.Models;
using Xunit;

namespace CommonUnitTests.Models {

    public class ContactValidatorTest {
        private string Name = "Rafael";
        private string Email = "rafael@brasil.com";
        private string Mobile = "1234567890";
        private ContactValidator subject = new ContactValidator();

        [Fact]
        public void AContactWithAllFieldsFilledInIsValid() {
            // Given a fully filled in and valid contact
            var contact = new Contact(Name, Email, Mobile);

            // Then the validator states the contact is valid
            Assert.True(subject.IsValid(contact));
        }

        [Fact]
        public void AContactWithNullNameIsInvalid() {
            // Given a contact with null name
            var contact = new Contact(null, Email, Mobile);

            // Then the validator states the contact is invalid
            Assert.False(subject.IsValid(contact));
        }

        [Fact]
        public void AContactWithEmptyNameIsInvalid() {
            // Given a contact with empty name
            var contact = new Contact("", Email, Mobile);

            // Then the validator states the contact is invalid
            Assert.False(subject.IsValid(contact));
        }
    }
}