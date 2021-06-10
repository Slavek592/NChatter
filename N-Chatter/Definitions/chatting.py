#!/usr/bin/python3
import requests
import json
import random
import time
from .printing import *


def group_chatting(user, group):
    time.sleep(2)
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group["id"]) +
                                "/Messages", verify=False, headers=headers)
        try:
            answer = response.json()
            print("Messages:")
            for message_to_print in answer:
                print_message(message_to_print)
        except:
            print("No messages here.")
        while True:
            command = str(input("What do You wish? Reload messages, write a new one, find message by id,"
                                " update message, delete message, or exit back to the group menu?"))
            if command in ["r", "R", "reload", "Reload", "w", "W", "write", "Write", "e", "E", "exit", "Exit",
                           "m", "M", "menu", "Menu", "f", "F", "find", "Find", "u", "U", "update", "Update",
                           "d", "D", "delete", "Delete"]:
                break
            else:
                print("Unknown command.")
        if command in ["w", "W", "write", "Write"]:
            message = str(input("Input Your message here."))
            i = random.randint(0, 100000)
            while True:
                to_load = json.dumps({'id': i, "text": message})
                response = requests.post("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group["id"])
                                         + "/Messages", verify=False, data=to_load, headers=headers)
                if str(response.status_code) == "200":
                    break
                else:
                    i += 1
        elif command in ["u", "U", "update", "Update"]:
            while True:
                i = str(input("Enter ID of the message."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            message = str(input("Input new version of Your message here."))
            to_load = json.dumps({'id': i, "text": message})
            response = requests.put("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group["id"])
                                    + "/Messages/" + str(i), verify=False, data=to_load, headers=headers)
            if str(response.status_code) != "200":
                print("Something went bad.")
        elif command in ["f", "F", "find", "Find"]:
            while True:
                i = str(input("Enter ID of the message."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Groups/" + str(group["id"])
                                    + "/Messages/" + str(i), verify=False, headers=headers)
            if str(response.status_code) == "200":
                print(response.text)
            else:
                print("Something went bad.")
        elif command in ["d", "D", "delete", "Delete"]:
            while True:
                i = str(input("Enter ID of the message to delete."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            while True:
                sure = str(input("Are You sure? You are going to delete a message. (y/N)"))
                if sure in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if sure in ["y", "Y"]:
                for j in range(10):
                    response = requests.delete("http://localhost:5000/User/" + str(user["id"]) + "/Groups/"
                                               + str(group["id"]) + "/Messages/" + str(i), verify=False,
                                               headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif j == 9:
                        "Something went wrong, try next time again."
        elif command in ["e", "E", "exit", "Exit", "m", "M", "menu", "Menu"]:
            break
        time.sleep(1)


def contact_chatting(user, contact):
    time.sleep(2)
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}
    while True:
        response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(contact["id"]) +
                                "/Messages", verify=False, headers=headers)
        try:
            answer = response.json()
            print("Messages:")
            for message_to_print in answer:
                print_message(message_to_print)
        except:
            print("No messages here.")
        while True:
            command = str(input("What do You wish? Reload messages, write a new one, find message by id,"
                                " update message, delete message, or exit back to the contact menu?"))
            if command in ["r", "R", "reload", "Reload", "w", "W", "write", "Write", "e", "E", "exit", "Exit",
                           "m", "M", "menu", "Menu", "f", "F", "find", "Find", "u", "U", "update", "Update",
                           "d", "D", "delete", "Delete"]:
                break
            else:
                print("Unknown command.")
        if command in ["w", "W", "write", "Write"]:
            message = str(input("Input Your message here."))
            i = random.randint(0, 100000)
            while True:
                to_load = json.dumps({'id': i, "text": message})
                response = requests.post("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/"
                                         + str(contact["id"]) + "/Messages", verify=False, data=to_load,
                                         headers=headers)
                if str(response.status_code) == "200":
                    break
                else:
                    i += 1
        elif command in ["u", "U", "update", "Update"]:
            while True:
                i = str(input("Enter ID of the message."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            message = str(input("Input new version of Your message here."))
            to_load = json.dumps({'id': i, "text": message})
            response = requests.put("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(contact["id"])
                                    + "/Messages/" + str(i), verify=False, data=to_load, headers=headers)
            if str(response.status_code) != "200":
                print("Something went bad.")
        elif command in ["f", "F", "find", "Find"]:
            while True:
                i = str(input("Enter ID of the message."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            response = requests.get("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/" + str(contact["id"])
                                    + "/Messages/" + str(i), verify=False, headers=headers)
            if str(response.status_code) == "200":
                print(response.text)
            else:
                print("Something went bad.")
        elif command in ["d", "D", "delete", "Delete"]:
            while True:
                i = str(input("Enter ID of the message to delete."))
                correct = True
                for letter in i:
                    if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                        correct = False
                        print("Incorrect input.")
                        break
                if correct:
                    i = int(i)
                    break
            while True:
                sure = str(input("Are You sure? You are going to delete a message. (y/N)"))
                if sure in ["y", "n", "Y", "N"]:
                    break
                else:
                    print("Unknown answer.")
            if sure in ["y", "Y"]:
                for j in range(10):
                    response = requests.delete("http://localhost:5000/User/" + str(user["id"]) + "/Contacts/"
                                               + str(contact["id"]) + "/Messages/" + str(i), verify=False,
                                               headers=headers)
                    if str(response.status_code) == "200":
                        break
                    elif j == 9:
                        "Something went wrong, try next time again."
        elif command in ["e", "E", "exit", "Exit", "m", "M", "menu", "Menu"]:
            break
        time.sleep(1)
