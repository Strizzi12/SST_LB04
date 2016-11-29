# SST_LB04

Übung
Aktienmarkt
Spezielle Software Technologien
Wintersemester 2016/17
Cloud Computing &
Cloud Services
Erstellt von:
DI Eduard Hirsch

Übungsinhalt:
Ziel der Übung ist es, mit Hilfe von Cloud Services einen Aktienhandel zu simulieren. Das Service Ihrer Bank soll nun um die Möglichkeit erweitert werden, ein Aktiendepot pro Kunde anzulegen. Von diesem Depot aus soll Aktienhandel mit verschiedenen Märkten (Ländern) ermöglicht werden.
Die Gruppen aus den letzten Beispielen bleiben bestehen!
Wir werden die Amazon Webservices für die Umsetzung benutzen.

Elemente:
Bankendepot Applikation:
Kann eine eigene Instanz sein oder mit Ihrer „Banking“-Applikation verbunden. Auf jeden Fall muss
der User die Möglichkeit haben ein Depot anzulegen und seine Aktien und Kurse einzusehen. Inkl.
ursprünglicher Preise und Kurse, damit eine Gewinn- bzw. Verlustrechnung vorgenommen werden
kann.

Kauf und Verkauf von Aktien:
Der Kunde soll über das Orderbuch der Börse Käufe und Verkäufe tätigen können, die natürlich
dementsprechend mit seinem Bankendepot abgeglichen werden.
Kunden sollen aber auch die Möglichkeit haben mit Aktien aus anderen Ländern, also von Börsen
anderer Gruppen, zu handeln. Die Banken kennen die Online Börsenservices der anderen Länder =>
URLs können ausgetauscht werden, und es muss daher keine zentrale Registration geben.

Börseninstanz:
Die Börseninstanz hat alle Informationen für eine länderspezifische Aktie, speichert alle (Ver-)Käufe
und natürlich den Kursverlauf.
Eine weitere der Hauptaufgaben von Wertpapierbörsen ist die ordnungsgemäße Kursfeststellung
eines Wertpapiers (Aktie). Die Börseninstanz legt den Kurs einer Aktie so fest, dass der
höchstmögliche Umsatz aus den vorhandenen Kauf- und Verkaufsaufträgen erzeugt wird. Diese
Kursfeststellung wird automatisch im 15 Minutentakt aufgerufen. Dazu siehe auch:
http://www.finanzen.net/special/nachricht/Wie_kommt_ein_Aktienkurs_zustande_-3-3968223
oder
http://www.charttec.de/html/info_kursfeststellung.php
Die Börse soll für ein Land zuständig sein, dass Sie sich selbst aussuchen. Sprechen Sie jedoch mit den
anderen Gruppen, dass Sie unterschiedliche Ländernamen verwenden.

Das Orderbuch:
Das Orderbuch liegt in der Börse des jeweiligen Landes vor. Es enthält eine Liste von Käufern mit
deren potentiellen Kaufordern und Verkäufer mit deren entsprechenden Verkäufen. Wichtig ist
natürlich ein Zeitstempel, wann die jeweiligen Orders/Aufträge eingegangen sind.
Achten Sie darauf, dass Anfragen an das Orderbuch einem Standard folgen, auf den Sie sich
gruppenübergreifend einigen.
