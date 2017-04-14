# stackattack
A "Guess the StackOverflow accepted answer" webapp game

## Build Instructions ##

* Restore the Nuget packages
* Run ```npm install``` in the ```stackattack``` directory.

The app uses an SQLite database which it builds in the ```stackattack```
directory if it can't be found.

## Settings

The following settings are available in the ```web.config```

* MaxQuestions
  * The amount of questions the app will retrieve and store.
  * Defaults to 20
* SQLiteDatabasePath
  * The path to your custom SQLite database file.
  * Defaults to ```stackattack\stackattack.sqlite```

## Sidenotes

The ```stackattack\Scripts\src``` directory contains the outline
of an angular version of the app, but as I do not know angular,
it was really slowing me down. Hence, I created ```src2``` which
is a more rough hewn, get it done JS approach.
