#!/usr/bin/python3
from Definitions import login
from Definitions import actions

print("Welcome, user! Nice to see You!")
print("It is N-Chatter, an application made by Viacheslav Nikiforov.")

user = login.main()
actions.main_menu(user)

print("Thank You for using the application!")
print("Have a nice day!")
print("Made by Viacheslav Nikiforov.")
