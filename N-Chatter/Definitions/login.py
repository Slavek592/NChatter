#!/usr/bin/python3
import requests
import json


def main():
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}

    print("Please, log in (or sign up).")
    while True:
        command = str(input("Do You wish to log in, or to sign up?"))
        if command in ["l", "s", "login", "signup", "log in", "sign up", "log", "sign",
                       "L", "S", "Login", "Signup", "Log in", "Sign up", "Log", "Sign"]:
            break
        else:
            print("Unknown command.")

    if command in ["l", "login", "log in", "log", "L", "Login", "Log in", "Log"]:
        user_id = login()
    else:
        user_id = signup()

    response = requests.get("http://localhost:5000/User/" + str(user_id), verify=False, headers=headers)
    user = response.json()

    print("Welcome, " + user["username"] + "!")

    return user


def login():
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}

    while True:
        while True:
            user_id = str(input("Please, print Your ID."))
            correct = True
            for letter in user_id:
                if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                    correct = False
                    print("Incorrect input.")
                    break
            if correct:
                user_id = int(user_id)
                break
        password = str(input("Please, input Your password."))

        to_load = json.dumps({'id': int(user_id), 'password': password})
        response = requests.post("http://localhost:5000/User/Login", data=to_load, verify=False, headers=headers)
        if str(response.status_code) == "200":
            return user_id
        else:
            print("Something went wrong. Try again.")


def signup():
    headers = {'Content-type': 'application/json', 'Accept': 'application/json'}

    while True:
        while True:
            user_id = str(input("Please, print Your ID."))
            correct = True
            for letter in user_id:
                if letter not in ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]:
                    correct = False
                    print("Incorrect input.")
                    break
            if correct:
                user_id = int(user_id)
                break

        username = str(input("Please, input Your username."))
        password = str(input("Please, input Your password."))

        to_load = json.dumps({'id': int(user_id), 'username': username, 'password': password})
        response = requests.post("http://localhost:5000/User", data=to_load, verify=False, headers=headers)
        if str(response.status_code) == "200":
            return user_id
        else:
            print("Something went wrong. Try again.")
