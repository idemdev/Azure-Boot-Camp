# My Tasks Lab #

## Prerequisites ##

Visual Studio 2015 or 2015 Community (or earlier)

[Github](https://desktop.github.com/) for Windows

Github account

Azure subscription

## Setup ##

Navigate to https://github.com/idemdev/Azure-Boot-Camp

![](http://i.imgur.com/YA4t1eg.jpg)

Clone and then open in desktop.

This will download the repository to a folder on your laptop.

Navigate to the folder.

![](http://i.imgur.com/NayIKq3.jpg)

Click on the folder.

Navigate down to the solution file MyTasks.sln

Open this in VS. Run as administrator.

Rebuild the solution. This will download the required NuGet packages.

From "View", open "Server Explorer".

![](http://i.imgur.com/9bA7Dcu.jpg)

Login to your Azure subscription. You can ignore this step if you want. When you publish it will automatically ask you to log in. 

![](http://i.imgur.com/yTy1tIb.jpg)

Right-click on MyTasks.ResourceGroup.

Click Deploy and fill in the fields.

![](http://i.imgur.com/ka8O9jE.jpg)

Click "Edit Parameters"

![](http://i.imgur.com/ku1pkRY.jpg)

Change the default value for appname e.g. add initials and random number.

Click Save.

Click OK.

Follow progress in the Output window.

It should end with "Successfully deployed template 'website.json' to resource group 'Default-Web-SoutheastAsia'." or whatever location you picked.

In Server Explorer, click Refresh.

You should see the two web sites.

![](http://i.imgur.com/EEPHjF4.jpg)

Right-click on web-azure-bootcamp-auckland ...

Click Download Publish Profile and save.

In Solution Explorer right-click on MyTasks.Web and select Publish.

Select Import.

![](http://i.imgur.com/wlFntcp.jpg)

Browse to the publish profile you saved above.

![](http://i.imgur.com/mtSRlaP.jpg)

Then select Publish.

Repeat the process as above for api-azure-bootcamp-auckland ...

The web site and web service should now be deployed.

Navigate to the [Azure portal](https://portal.azure.com) and login with your credentials.

Click App Services.

![](http://i.imgur.com/yaTiiRr.jpg)

Then click on your web site.

![](http://i.imgur.com/SwpTdRm.jpg)

Then click on Application Settings.

![](http://i.imgur.com/nS0I8GT.jpg)

Then enter a key of apiurl and the URL of the api site.

![](http://i.imgur.com/wmb2xW7.jpg)

Then Save out.

Navigate to the web site e.g.

http://web-azure-bootcamp-auckland-xxx-1234.azurewebsites.net/

![](http://i.imgur.com/0VwTGPo.jpg)

Enter a task e.g. "Enjoy the bootcamp!"

Click "Add".

![](http://i.imgur.com/IEZz2FM.jpg)

Click "Complete".

![](http://i.imgur.com/q35bxb0.jpg)

Congratulations. You have finished this section :-)

# Xamarin #

## Prerequisites ##

Visual Studio 2015 with Xamarin extension installed.

Android emulator with Wifi enabled, or an Android device with wifi enabled

and connected to the internet,


## Setup ##

Set the MyTasks.AndroidApp project as the start up project,

Select the Android device or eumlator and click run,

The app will automatically be deployed to your device

Have fun adding all your ToDo s :)



# Dev Ops #

## Prerequisites ##

## Setup ##




