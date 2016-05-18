namespace ContactList.Models
{
	using ContactListUnitTest;
	using System;
	using System.Collections.Generic;

	public class ContactRepositoryStub : Stub<IContactRepository>, IContactRepository
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;

		public ContactRepositoryStub()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
		}

		public IList<Contact> GetContacts()
		{
			IList<Contact> result;
			this.InvokeMember("GetContacts", new object[] {  }, out result);
			return result;
		}
		public bool SaveContact(Contact contact)
		{
			bool result;
			this.InvokeMember("SaveContact", new object[] { contact }, out result);
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
		public class CountCallers
		{
			private readonly ContactRepositoryStub parent;
			internal CountCallers(ContactRepositoryStub parent)
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
			public CountCallers GetContacts()
			{
				this.parent.Called("GetContacts");
				return this;
			}
			public CountCallers SaveContact(Contact contact)
			{
				this.parent.CalledWith("SaveContact", contact);
				return this;
			}
			public CountCallers SaveContact()
			{
				this.parent.Called("SaveContact");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly ContactRepositoryStub parent;
				private readonly int count;
				internal CountCallerMethods(ContactRepositoryStub parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods GetContacts()
				{
					this.parent.Called(this.count, "GetContacts");
					return this;
				}
				public CountCallerMethods SaveContact(Contact contact)
				{
					this.parent.CalledWith(this.count, "SaveContact", contact);
					return this;
				}
				public CountCallerMethods SaveContact()
				{
					this.parent.Called(this.count, "SaveContact");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly ContactRepositoryStub parent;
			internal CountCalls(ContactRepositoryStub parent)
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
				private readonly ContactRepositoryStub parent;
				private readonly int position;
				internal CountCallsMethods(ContactRepositoryStub parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation GetContacts()
				{
					return this.parent.GetCall(this.position, "GetContacts");
				}
				public MemberInvocation SaveContact()
				{
					return this.parent.GetCall(this.position, "SaveContact");
				}
			}
		}
		public class Handlers
		{
			private readonly ContactRepositoryStub parent;
			internal Handlers(ContactRepositoryStub parent)
			{
				this.parent = parent;
			}
			public Handlers GetContacts(Func<IList<Contact>> action)
			{
				this.parent.Handle<IList<Contact>>("GetContacts", action);
				return this;
			}
			public Handlers SaveContact(Func<Contact, bool> action)
			{
				this.parent.Handle<Contact, bool>("SaveContact", action);
				return this;
			}
		}
	}
}
