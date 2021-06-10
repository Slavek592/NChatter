#!/usr/bin/python3


def print_user(user_to_print):
    print(str(user_to_print["id"]) + ". " + user_to_print["username"])


def print_group(group_to_print):
    print(str(group_to_print["id"]) + ". " + group_to_print["name"])


def print_message(message_to_print):
    print(str(message_to_print["id"]) + ". " + str(message_to_print["sentFrom"]) + ": " + message_to_print["text"])
    print("    " + message_to_print["sentTime"] + ", " + message_to_print["status"] + "\n")
