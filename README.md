# Currency converter
Currency converter WPF application. Data is received from public APIs. Users can change/add/delete currencies and conversion happen on user's input. The application keeps settings when closed.

Reference API list: https://github.com/public-apis/public-apis#currency-exchange

# Todo list:

Change UI/UX design
Save history
Show in another window 2-3 preferred currency exchanges simultaneously
Implement a mechanism of refresh, when exchange rates are changed
Implement a chart with the history of currencies and the possibility to navigate on it
Implement a calendar with the history of currencies
Improvements on responsiveness part
Solve any bugs
Add unit tests



# Framework/Architecture:

Main frameworks I’ve used in developing this app are .NET Framework and its subset, WPF Framework. When we talk about Windows desktop applications, this is one of the best combinations to choose from. Of course, there are other options too, as Windows forms (in my opinion WPF is more powerful) and UWP (new and targets also mobile devices, IoT, Xbox, yet it is supported by only >= Windows 10).
Besides the framework I’ve used, I would like to mention some other stuff I’ve used in terms of architecture, as follows.

I opted out for MVVM pattern, which from my point of view it should be mandatory when we talk about WPF applications. MVVM is great in terms of distributing responsibilities between classes: Models, Views and ViewModels which respects the Separation of Concerns principle. This leads to the possibility of future drastic changes to the UI.

I’ve used the ViewModel first approach where ViewModel is responsible for creating the View. This way views are changed based on available ViewModels.

I’ve used Json.NET library (from NuGet) as it provides an easy and efficient way to serialize/deserialize data from JSON to .NET and back

In order to keep user’s settings I’ve used Properties object as it is ready to use and quite simple in implementing. I did not see the need for saving through serialization, in JSON/XML, databases, etc.

I’ve also reduced to minimum, the usage of code behind which would create dependencies and break the MVVM pattern. As a solution to this, I’ve opted out to use Commands (ICommand) instead of most events.

Other techniques that can be found in the app: 
- use of simple, clear and clean IClose (in house interface) instead of VM manipulation from code behind Window Closing event. 
- use of asynchronous in order to keep the UI responsive. 
- use of Parallel.ForEach to iterate by using multiple threads. 
- use of AttachedProperties to eliminate any VM changes in code behind. 
- implemented ImageButton custom control, to have a button with images representing 4 states: normal, hover, pressed and disabled.
- text boxes from MainCurrencyConverter.xaml where a challenge to design. Because through binding there is set the IsAsync property to True, when user was typing values, the caret index always jumped to index 0. The solution I found was to implement an AttachedProperty (NonIntrusiveText) which fixed it.
- added a small animation for changing views, which can be deactivated from settings view.

