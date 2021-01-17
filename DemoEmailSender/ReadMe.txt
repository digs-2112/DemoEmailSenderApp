C# - Excercise Description

Solution Name: DemoEmailSender
1) Please use this GitHub link to Clone the repository
	https://github.com/digs-2112/DemoEmailSenderApp.git
2) After cloning the repository, please set your gmail account username and password in web.config:
	<system.net>
    <mailSettings>
      <smtp from="YourEmail@gmail.com">
        <network host="smtp.gmail.com"
                 port="587"
                  userName="YourEmail@gmail.com"
                  password="YourPassword"
                  enableSsl="true"/>
      </smtp>
    </mailSettings>
  </system.net>
3) I have created "MediaFiles" folder in the root of solution which contains few test files of csv, excel and pdf. You can add files if you wish.
4) For build and run application use F5
5) I didn't created Unit test project for this solution, so testing is solely depends upon reviewers discretion.
6) I have used JQuery datatable in order to display the file list.
7) The Bootstrap popup is used to show and input email details.
8) I have used ajax to send email.
9) Here is the validation detail:
   a) To Email is required
   b) To, CC and BCC is validated with email regex
   c) Multple email addresses can be inputted in To, CC or BCC with comma, semi-colon or white space and they are validated with email regex.
10) Once email is sent successfully, alert is shown in the browser.
11) Validation error message can be seen inside Bootstrap popup.



