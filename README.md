# xml-to-json-parser
.NET Framework Application that parse xml objects to JSON

The .NET Framework software takes XML file "input.xml" from the current directory and parses it to "output.json". It reads the following keywords: 
-object
-obj_name
-field
-name
-value
-type
It ignores objects that:
- have the same object name
- have an unsupported type of data (can be string or int)
- fields that have unsupported keywords
- objects that are not correct
- everything that contains non-printable characters
Example input:
``` json
<object>
<obj_name>hero_profile 1</obj_name>
<field>
<name>favorite_fruit</name>
<type>string</type>
<value>Truskawa</value>
</field>
<field>
<name>hero_name</name>
<type>string</type>
<value>batman</value>
</field>
<field>
<name>age</name>
<type>int</type>
<value>32</value>
</field>
</object>
<object>
<obj_name>hero_profile Z</obj_name>
<field>
<name>favorite_fruit</name>
<type>string</type>
<value>Kokos</value>
</field>
<field>
<name>hero_name</name>
<type>string</type>
<value>Tomy Lee Jonse w sciganym</value>
</field>
<field>
<name>age</name>
<type>int</type>
<value>47</value>
</field>
</object>
```
```xml
Example output:
{
"hero_profile 1": {
"favorite_fruit": "Truskawa",
"hero_name": "batman",
"age": 32
},
"hero_profile Z": {
"favorite_fruit": "Kokos",
"hero_name": "Tomy Lee Jonse w sciganym",
"age": 47
}
}
```
