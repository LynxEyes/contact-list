namespace ContactList.Models
{
	using ContactListUnitTest;
	using System.Collections.Generic;

	public class ContactValidatorSpy : Spy<IContactValidator>, IContactValidator
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public ContactValidatorSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
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
		public class CountCallers
		{
			private readonly ContactValidatorSpy parent;
			internal CountCallers(ContactValidatorSpy parent)
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
				private readonly ContactValidatorSpy parent;
				private readonly int count;
				internal CountCallerMethods(ContactValidatorSpy parent, int count)
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
			private readonly ContactValidatorSpy parent;
			internal CountCalls(ContactValidatorSpy parent)
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
				private readonly ContactValidatorSpy parent;
				private readonly int position;
				internal CountCallsMethods(ContactValidatorSpy parent, int position)
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
	}
}
