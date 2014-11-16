# Kangaroo TimeTracker

Kangaroo is a simple time tracker wich allows add hour in a project by day.

###Screenshots

####The home page
![Home screenshot](/Screenshots/Home.PNG?raw=true "Home")

####The dashboard
![Dashboard screenshot](/Screenshots/Dahsboard.PNG?raw=true "Dashboard")

###Intention of the project

This project is a proof of concept to try a few new concepts. 
The project use an [CQS](http://en.wikipedia.org/wiki/Command%E2%80%93query_separation) approach and MongoDb to store information. 

###Open source dependencies

The TimeTracker use a few open source components.

From [nuget](https://www.nuget.org/):

* **DotNet.Highcharts** for charts in dashboard
* **bootstrap** for styling
* **[bootswatch](http://bootswatch.com/)** for theming
* **FluentValidation** for command validations
* **FontAwesome**
* **Humanizer**
* **SimpleInjector** for IoC
* **toastr** for notifications
* **Moq** for mocking
* And many more...

To check the complete view see the [package.config file](/Kangaroo/packages.config?raw=true "All packages")

The **CommandProcessor** was taken from [EFMVC repository](https://github.com/shijuvar/EFMVC.Azure/tree/master/EFMVC.CommandProcessor) and from [FNHMVC repository](https://fnhmvc.codeplex.com/)


###Pending tasks

* Implement Authentication
* Implement Authorization for diferent features
* ...
