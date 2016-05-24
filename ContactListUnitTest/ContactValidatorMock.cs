namespace ContactList.Models
{
	using ContactListUnitTest;
	using System;
	using System.Collections.Generic;

	public class ContactValidatorMock : Mock<IContactValidator>, IContactValidator
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public ContactValidatorMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
		}

		public bool IsValid(Contact contact)
		{
			bool result;
			this.InvokeMember("IsValid", new object[] { contact }, out result);
			return result;
		}
		public CountCallers HasBeenCalled()
		{
			return this.countCallers;
		}
		public CountCalls GetCalls()
		{
			return this.countCalls;
		}
		public Handlers AddHandlers()
		{
			return this.handlers;
		}
		public Verifications AddVerifications()
		{
			return this.verifications;
		}
		public Verifiers Verify()
		{
			return this.verifiers;
		}
		public class CountCallers
		{
			private readonly ContactValidatorMock parent;
			internal CountCallers(ContactValidatorMock parent)
			{
				this.parent = parent;
			}
			public CountCallerMethods Once()
			{
				return new CountCallerMethods(this.parent, 1);
			}
			public CountCallerMethods Twice()
			{
				return new CountCallerMethods(this.parent, 2);
			}
			public CountCallerMethods Thrice()
			{
				return new CountCallerMethods(this.parent, 3);
			}
			public CountCallerMethods Times(int times)
			{
				return new CountCallerMethods(this.parent, times);
			}
			public CountCallers IsValid(Contact contact)
			{
				this.parent.CalledWith("IsValid", contact);
				return this;
			}
			public CountCallers IsValid()
			{
				this.parent.Called("IsValid");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly ContactValidatorMock parent;
				private readonly int count;
				internal CountCallerMethods(ContactValidatorMock parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods IsValid(Contact contact)
				{
					this.parent.CalledWith(this.count, "IsValid", contact);
					return this;
				}
				public CountCallerMethods IsValid()
				{
					this.parent.Called(this.count, "IsValid");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly ContactValidatorMock parent;
			internal CountCalls(ContactValidatorMock parent)
			{
				this.parent = parent;
			}
			public CountCallsMethods First()
			{
				return new CountCallsMethods(this.parent, 0);
			}
			public CountCallsMethods Second()
			{
				return new CountCallsMethods(this.parent, 1);
			}
			public CountCallsMethods Third()
			{
				return new CountCallsMethods(this.parent, 2);
			}
			public CountCallsMethods At(int position)
			{
				return new CountCallsMethods(this.parent, position);
			}
			public class CountCallsMethods
			{
				private readonly ContactValidatorMock parent;
				private readonly int position;
				internal CountCallsMethods(ContactValidatorMock parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation IsValid()
				{
					return this.parent.GetCall(this.position, "IsValid");
				}
			}
		}
		public class Handlers
		{
			private readonly ContactValidatorMock parent;
			internal Handlers(ContactValidatorMock parent)
			{
				this.parent = parent;
			}
			public Handlers IsValid(Func<Contact, bool> action)
			{
				this.parent.Handle<Contact, bool>("IsValid", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly ContactValidatorMock parent;
			internal Verifications(ContactValidatorMock parent)
			{
				this.parent = parent;
			}
			public Verifications IsValid(Contact contact)
			{
				this.parent.AddVerification("IsValid", contact);
				return this;
			}
		}
		public class Verifiers
		{
			private readonly ContactValidatorMock parent;
			internal Verifiers(ContactValidatorMock parent)
			{
				this.parent = parent;
			}
			public Verifiers IsValid(Contact contact)
			{
				this.parent.Verify("IsValid", contact);
				return this;
			}
		}
	}
}
