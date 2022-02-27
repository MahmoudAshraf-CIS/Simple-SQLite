# Simple-SQLite
Simple SQLite was created following the [Tutorial](https://www.youtube.com/watch?v=8bpYHCKdZno&ab_channel=Digestible)
[SQLite](https://sqlite.org/index.html) is one of the most popular options for lightweight databases, so it's a great candidate for use in a Unity application when you want something a little more robust than a structured text file (JSON, XML, YAML, etc.) or C#'s serialized objects.

```bash
[unity project folder]
-└─[Assets]
    └─[Plugins]
        └─[SQLite]
            Mono.Data.Sqlite.dll
            └─[x64]
                sqlite3.dll
                sqlite3.def
            └─[x86]
                sqlite3.dll
                sqlite3.def
```

i tried to follow another guide [here](https://javadocmd.com/blog/how-to-set-up-sqlite-for-unity/) but it's a bit outdated i think so i droped it


only tested on the editor though

the db is located at
```bash
C:\Users\<user name>\AppData\LocalLow\DefaultCompany\Simple-SQLite
```

to view the DB you can use SQLCipher `just install SQLite`
