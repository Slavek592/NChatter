#!/usr/bin/python3
import requests
import json
import random
from .printing import *
from .chatting import *


def main_menu(user):
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        response = requests.get("http://localhost:5000/User/" + str(user["id"]), verify=False, headers=headers)
        user = response.json()

        while True:
            print("What do You wish?")
            print("You can open Your user menu, open Your contacts, open Your groups and turn off the application.")
            command = str(input(""))
            if command in ["u", "m", "c", "g", "user", "menu", "contact", "contacts", "group", "groups",
                           "U", "M", "C", "G", "User", "Menu", "Contact", "Contacts", "Group", "Groups",
                           "t", "T", "e", "E", "turn", "Turn", "turn off", "Turn off", "exit", "Exit",
                           "user menu", "User menu"]:
                break
            else:
                print("Unknown command.")
        if command in ["u", "U", "m", "M", "user", "User", "menu", "Menu", "user menu", "User menu"]:
            user_menu(user)
        elif command in ["c", "C", "contact", "Contact", "contacts", "Contacts"]:
            contact_menu(user)
        elif command in ["g", "G", "group", "Group", "groups", "Groups"]:
            group_menu(user)
        else:
            break


def user_menu(user):
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        while True:
            print("What do You wish?")
            print("You can check Your account, change Your account, search by username and exit to the main menu.")
            command = str(input(""))
            if command in ["m", "main", "menu", "M", "Main", "Menu", "e", "E", "exit", "Exit", "main menu", "Main menu",
                           "check", "Check", "change", "Change", "s", "S", "search", "Search"]:
                break
            else:
                print("Unknown command.")
        if command in ["check", "Check"]:
            response = requests.get("http://localhost:5000/User/" + str(user["id"]), verify=False, headers=headers)
            print(response.text)
        elif command in ["change", "Change"]:
            while True:
                username = str(input("Do You wish to change Your username? (y/N)"))
                if username in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if username in ["y", "Y"]:
                username = str(input("Print Your new username."))
                for i in range(10):
                    to_load = json.dumps({'id': int(user["id"]), 'username': username})
                    response = requests.put("http://localhost:5000/User/" + str(user["id"]), verify=False,
                                            data=to_load, headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif i == 9:
                        "Something went wrong, try next time again."
            while True:
                password = str(input("Do You wish to change Your password? (y/N)"))
                if password in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if password in ["y", "Y"]:
                password = str(input("Print Your new password."))
                for i in range(10):
                    to_load = json.dumps({'id': int(user["id"]), 'password': password})
                    response = requests.put("http://localhost:5000/User/" + str(user["id"]), verify=False,
                                            data=to_load, headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif i == 9:
                        "Something went wrong, try next time again."
        elif command in ["s", "S", "search", "Search"]:
            username = str(input("Input the username of the user."))
            response = requests.get("http://localhost:5000/User/Name/" + username, verify=False, headers=headers)
            if str(response.status_code) == "200":
                answer = response.json()
                for user_to_print in answer:
                    print_user(user_to_print)
            else:
                print("Not found.")
        else:
            break


def group_menu(user):
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Groups", verify=False,
                                headers=headers)
        try:
            print("Here are all the Your groups:")
            answer = response.json()
            for group_to_print in answer:
                print_group(group_to_print)
        except:
            print("You have no groups at the moment.")
        while True:
            print("What do You wish?")
            print("You can open groups, modify groups, create groups, delete groups and exit to the main menu.")
            command = str(input(""))
            if command in ["e", "E", "exit", "Exit", "main", "Main", "menu", "Menu", "o", "O", "open", "Open",
                           "m", "M", "modify", "Modify", "c", "C", "create", "Create", "d", "D", "delete", "Delete"]:
                break
            else:
                print("Unknown command.")
        if command in ["o", "O", "open", "Open"]:
            while True:
                group_id = str(input("Choose group by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group_id),
                                    verify=False, headers=headers)
            if str(response.status_code) == "200":
                group = response.json()
                print_group(group)
                response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group_id)
                                        + "/Members", verify=False, headers=headers)
                try:
                    print("Members:")
                    answer = response.json()
                    for user_to_print in answer:
                        print_user(user_to_print)
                except:
                    print("No members there.")
                group_chatting(user, group)
            else:
                print("Something went wrong. Try again.")
        elif command in ["modify", "Modify"]:
            while True:
                group_id = str(input("Choose group by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            group_name = str(input("Input new name for the group."))
            for i in range(10):
                to_load = json.dumps({'id': group_id, 'name': group_name})
                response = requests.put("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group_id),
                                        verify=False, data=to_load, headers=headers)
                if str(response.status_code) == "200":
                    break
                elif i == 9:
                    "Something went wrong, try next time again."
        elif command in ["d", "D", "delete", "Delete"]:
            while True:
                group_id = str(input("Choose group by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            while True:
                sure = str(input("Are You sure? You are going to delete a group. (y/N)"))
                if sure in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if sure in ["y", "Y"]:
                for i in range(10):
                    response = requests.delete("http://localhost:5000/User/" + str(user["id"]) + "/Groups/"
                                               + str(group_id), verify=False, headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif i == 9:
                        "Something went wrong, try next time again."
        elif command in ["c", "C", "create", "Create"]:
            group_name = str(input("Please, input name for Your group."))
            members = []
            while True:
                while True:
                    new = str(input("Do You want to add user to the group? (y/N)"))
                    if new in ["y", "n", "Y", "N"]:
                        break
                    else:
                        print("Unknown answer.")
                if new in ["n", "N"]:
                    break
                while True:
                    user_id = str(input("Enter user ID."))
                    correct = True
                    for letter in user_id:
                        if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                            correct = False
                            print("Incorrect input.")
                            break
                    if correct:
                        user_id = int(user_id)
                        members.append({"id": user_id})
                        break
            i = random.randint(0, 100000)
            while True:
                to_load = json.dumps({'id': i, 'name': group_name, 'members': members})
                response = requests.post("http://localhost:5000/User/" + str(user["id"]) + "/Groups/",
                                         verify=False, data=to_load, headers=headers)
                if str(response.status_code) == "200":
                    break
                else:
                    i += 1
        else:
            break


def contact_menu(user):
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Contacts", verify=False,
                                headers=headers)
        try:
            print("Here are all the Your contacts:")
            answer = response.json()
            for contact_to_print in answer:
                print_group(contact_to_print)
        except:
            print("You have no contacts.")
        while True:
            print("What do You wish?")
            print("You can open contacts, modify contacts, create contacts, delete contacts and exit to the main menu.")
            command = str(input(""))
            if command in ["e", "E", "exit", "Exit", "main", "Main", "menu", "Menu", "o", "O", "open", "Open",
                           "m", "M", "modify", "Modify", "c", "C", "create", "Create", "d", "D", "delete", "Delete"]:
                break
            else:
                print("Unknown command.")
        if command in ["o", "O", "open", "Open"]:
            while True:
                group_id = str(input("Choose contact by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(group_id),
                                    verify=False, headers=headers)
            if str(response.status_code) == "200":
                contact = response.json()
                print_group(contact)
                print("Members:")
                response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(group_id)
                                        + "/Members", verify=False, headers=headers)
                answer = response.json()
                for user_to_print in answer:
                    print_user(user_to_print)
                contact_chatting(user, contact)
            else:
                print("Something went wrong. Try again.")
        elif command in ["modify", "Modify"]:
            while True:
                group_id = str(input("Choose contact by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            group_name = str(input("Input new name for the contact (it will look same for both)."))
            for i in range(10):
                to_load = json.dumps({'id': group_id, 'name': group_name})
                response = requests.put("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(group_id),
                                        verify=False, data=to_load, headers=headers)
                if str(response.status_code) == "200":
                    break
                elif i == 9:
                    "Something went wrong, try next time again."
        elif command in ["d", "D", "delete", "Delete"]:
            while True:
                group_id = str(input("Choose contact by ID."))
                correct = True
                for letter in group_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    group_id = int(group_id)
                    break
            while True:
                sure = str(input("Are You sure? You are going to delete a contact. (y/N)"))
                if sure in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if sure in ["y", "Y"]:
                for i in range(10):
                    response = requests.delete("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/"
                                               + str(group_id), verify=False, headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif i == 9:
                        "Something went wrong, try next time again."
        elif command in ["c", "C", "create", "Create"]:
            group_name = str(input("Please, input name for Your contact (it will look same for both)."))
            members = []
            while True:
                user_id = str(input("Enter user (You will chat with) ID."))
                correct = True
                for letter in user_id:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    user_id = int(user_id)
                    members.append({"id": user_id})
                    break
            i = random.randint(0, 100000)
            while True:
                to_load = json.dumps({'id': i, 'name': group_name, 'users': members})
                response = requests.post("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/",
                                         verify=False, data=to_load, headers=headers)
                if str(response.status_code) == "200":
                    break
                else:
                    i += 1
        else:
            break
