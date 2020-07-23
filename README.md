# Table of Contents
1. [Design decisions / Assumptions](#designDecision)
2. [Testing Strategies](#testingStrategies)
3. [Deployment / Automation Strategies](#deploymentStrategies)
4. [Operation Support Considerations](#operationSupport)
5. [Issues Encounted](#issuesEncounted)

<a name="designDecision"></a>
## 1. Design decisions / Assumptions

#### a. Log Submission Portal 

##### Web Framework

###### ASP.NET Core MVC Web Application 

The underlying Log Submission Portal was build with a .NET Core MVC architecture, this allowed for a quick development and deployment and meant that the web application can be easily enhanced for future work. MVC allows for a seperation of concerns with the components/logic and can be easily modified or debugged if required. The routing that is used in the Controller performed some basic validation on the POST submission of the Log Message, this sort of validation would include ensuring that the user inputs data into the fields and would prevent a user from creating a Service Ticket Log if it is invalid. 

![Invalid Submission](/doc/Invalid%20Submission.PNG)

#### S3 Bucket Integration
###### AWS SDK for .NET

Amazon offers an easy to use SDK for integrating the Amazon S3 service to the Log Submission Portal, this allowed for a quick development turnaround for this portal and the uploading of a Log Ticket could be handled on the Server Side of the application. Seperating this function meant that it could be prototyped as a simple program first to ensure that it was possible to upload a file to the S3 Bucket and once that was confirmed, then it could be integrated into the web application. The Subject field is used as a the Key for the Amazon S3 Object, this is used later as part of the message body for the SMS notification function.

#### Azure App Service
##### Hosting service for Log Submission Portal

Azure was chosen as it was quick and efficient to publish to from Visual Studio allowing for a quick turn around. 

#### b. Amazon S3 Bucket
###### Events

To handle the upload of a new Log File from the Log Submission Portal the Bucket was configured to send a notification when a PUT event has occured. This event will only be triggered when a new log file with the suffix ".log" is uploaded, which is then sent to a Lamdba function. The suffic check is a simple form of validation to elimate unwanted notifications of objects that may have been uploaded accidently or are not a correct format. 

###### Lamba Function

To notify a service manager about a new Log File that has been submitted, a simple Python lambda function was created to extract the S3 Object Key name from the event data. That Object Key name would be used as part of the message body for the Twilio function. The script will attempt to POST a message to the Twilio account that contains the Service Managers mobile number. An example of the message would be "New log message with subject: " + [KeyName]
A Lambda Python function was used for simplicity purposes and to reduce the cost of hosting that functionality, there is no need to run a virtual machine 24/7 for this particular function.

#### c. Twilio

As per the specification Twilio was used as the SMS notification functionality, it offers a simple rest API that accomodates messages being sent to a url.

---

<a name="testingStrategies"></a>
## 2. Testing Strategies

#### a. S3 Bucket
##### i. .NET Core Component
To develop the solution for this, a small program was used to quickly test that the Amazon SDK was working as expectecd when trying to upload a sample log file.

##### ii. Lambda Event Handler
The first test performed was to ensure the script would send a set message through the Twilio API url, a "Hello World" style test was done. The second test involved using the inbuilt test functionality of the Python Lamdba tool, this had a JSON object that could be parsed in and this allowed for seperate unit testing of that script. The third test involved using the small program that was developed for the S3 functionality and test that an SMS notification would be generated upon a test Log file being uploaded. 

##### iii. Twilio SMS Notification
Another .NET program was used to push a message to the Twilio API, this was a quick way to test that the account was working and that the phone number used for the tests would receive the messages.

##### b. ASP.NET Core MVC Web Application 
During development of the web application simple user tests were performed, this included inputting incorrect data to the fields of the form, and testing the error page would display if the S3 bucket had failed.

---

<a name="deploymentStrategies"></a>
## 3. Deployment / Automation Strategies

#### a. S3 Bucket 
##### i. .NET Core Component
Unit testing could be implemented here for basic functionality such as the JSON parser to ensure the results were as expected. Integration testing could be used to check the integrity of the files being uploaded. An example might be to Upload a file, download that file and compare the fields.

##### ii. Lambda 
It might be possible to implement a form of automated unit testing on the Python script used to handle the event trigger, this would require further research and work.

---

<a name="operationSupport"></a>
## 4. Operation Support Considerations

#### a. ASP.NET Core MVC Web Application 
Issues relating to the Log Submission Portal would rely on a seperate issue tracking system, potentially an internal based system used by testers and an external used by customers. Those issues can then be assessed by a developer and then addressed if required.

Feature requests could also be handled internally and externally, a Product Owner who might want to implement a new feature for a future release cycle could submit proposals to an internal request board (Jira or Trello). A similiar system could be implemented for external feature reqests.

#### b. Amazon S3

#### c. Amazon Lambda

#### d. Twilio
---

<a name="issuesEncounted"></a>
## 5. Issues Encounted
The Amazons S3 Bucket was the biggest issue encountered for this piece of work, determining how to get IAM credentials working for the .NET integration required a significant chunk of time in order to upload a file. This was a result of not being experienced with that component. As previously mentioned, a sample application was written for testing and education purposes. Thankfully there is a lot of documentation available for .NET integration of the S3 Bucket component and this very helpful to get that part of the system working.   
