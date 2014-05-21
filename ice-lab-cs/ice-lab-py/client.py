import Ice, sys, Bank


def start():

	#print(sys.argv)

	comm = Ice.initialize([sys.argv[2]])

	manager_string = "manager:tcp -h "+sys.argv[1]+" -p 10000:ssl -p 12001:udp -p 10000"

	proxy = comm.stringToProxy(manager_string)

	bank_manager = Bank.BankManagerPrx.checkedCast(proxy)

	s = input()
	while ( s != ""):
		
		if (s == "new"):

			name = input()
			surname = input()
			national_id = input()
			t = input()
			if (t == "SILVER"):
				acc_type = Bank.accountType.SILVER
			else:
				acc_type = Bank.accountType.PREMIUM

			personal_data = Bank.PersonalData(name,surname,national_id) 
			account_id = bank_manager.createAccount(personal_data, acc_type)
			print(account_id)


		elif (s == "info"):
			
			account_number = input()
			account_string = account_number+":ssl -h "+sys.argv[1]+" -p 10001"

			proxy = comm.stringToProxy(account_string)
			account = Bank.AccountPrx.checkedCast(proxy)

			print(account.getBalance())

		elif (s == "transfer"):
			pass

		s = input()


if __name__ == '__main__':
	start()