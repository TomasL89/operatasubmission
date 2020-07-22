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

###### <span></span> ASP.NET Core MVC Web Application 

The underlying Log Submission Portal was build with a .NET Core MVC architecture, this allowed for a quick development and deployment and meant that the web application can be easily enhanced for future work. MVC allows for a seperation of concerns with the components/logic and can be easily modified or debugged if required. The routing that is used in the Controller performed some basic validation on the POST submission of the Log Message, this sort of validation would include ensuring that the user inputs data into the fields and would prevent a user from creating a Service Ticket Log if it is invalid. 

![Invalid Submission](/assets/Invalid%20Submission.PNG)

#### S3 Bucket Integration
###### AWS SDK for .NET

Amazon offers an easy to use SDK for integrating the Amazon S3 service to the Log Submission Portal, this allowed for a quick development turnaround for this portal and the uploading of a Log Ticket could be handled on the Server Side of the application. Seperating this function meant that it could be prototyped as a simple program first to ensure that it was possible to upload a file to the S3 Bucket and once that was confirmed, then it could be integrated into the web application. The Subject field is used as a the Key for the Amazon S3 Object, this is used later as part of the message body for the SMS notification function.



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


---

<a name="deploymentStrategies"></a>
## 3. Deployment / Automation Strategies


---

<a name="operationSupport"></a>
## 4. Operation Support Considerations

---

<a name="issuesEncounted"></a>
## 5. Issues Encounted

