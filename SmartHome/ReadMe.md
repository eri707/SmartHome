# Smart Home Management System
This API is a middle tier application meant to help you manage your Smart Home. 

# Motivation
The purpose of this project is to practice building RESTful APIs using ASP.NET and SQL, and learning about the Xunit testing framework.

## Table of Contents
### General Info 
The code for this project is available for your perusal here. 

### Technologies
Project is created with:
* C# ASP.NET
* SQL
* Xunit testing framework
### SetUp
To run this project, clone the repository, then...

```
$ cd SmartHome
$ dotnet install
$ dotnet start

```
### Features
The API will have two categories; Rooms and Devices. You can have multiple rooms, and each room can have multiple devices. Devices are linked to rooms via the RoomId property. Users can add a device first, and they can add rooms where they want to use devices. 

