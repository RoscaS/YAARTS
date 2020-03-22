# Architecture

Une longue discussion à été mise en place au sujet de l'architecture. Initialement on hésitait très fortement pour partir sur unity DOTS et un pattern architectural de type ECS. Mais finalement, suite à la lecture de nombreuses ressources et entre autres l'excellent livre "Game Programming Patterns" de Robert Nystrom, nous avons décidé d'épouser une architecture de type "Component" qui est l'architecture qu'utilise Unity pour ses GameObjects. Nos entités seront des conteneurs à components.

## Pattern architectural
Le diagramme suivant présente l'architecture qui a finalement été décidée:

![](https://i.imgur.com/FGRo4Vh.png)

Les classes ne sont pas complètes et ne présentent que les membres importants pour permetre le travail en groupe.

Nous avons ici le coeur de la logique. La partie inputs et ses différents niveaux d'abstraction sur la gauche, au centre le système de selection et d'entités qui sont intimement liés par un pattern Composit. Les `Entity` elles-mêmes qui utilisent le pattern Component (Pattern plus spécifique aux jeux vidéos qui ne fait pas partie des patterns du gang of 4).

Nous n'avons que peu d'expérience sur la partie GUI de part nos prototypes et il nous est présentement impossible de réelement l'architecturer. Donc nous adoptons une stratégie qui consiste à l'avoir en tant que module faiblement couplé et de tatonner. Une fois les principales difficultés ciblées, une discussion est prévue afin de mettre en place un refactor de cette portion du code pour mettre en place une architecture plus extensible

La classe `Entity` comporte des membres (Components) dont le type de retour est suffixé d'un `?` cela veut dire que ces membres peuvent être null. Les différents components sont sur la droite du diagramme.

La communication se fait principalement par le biais d'un pattern Observer pour réduire le couplage et favoriser l'ajout de nouvelles fonctionnalités.


## IA des entitées

les entités aussi bien statiques que mobiles ont leur état régi par la machine d'état suivante:

![](https://i.imgur.com/fKvpIJg.png)

Cette machine d'état pilote entre autres le component `Animation` qui est en charge de la communication avec le système *Animator* d'Unity. Cette façon de faire nous permet d'éviter certaines limitations d'*Animator*.

De façon général, une approche code est privilégiée.

Par exemple, c'est notre code qui pilote Animator et non l'inverse, nous sommes d'avis qu'il est plus facile d'avoir toute l'information sous les yeux sans devoir cliquer à gauche et à droite dans l'UI d'Unity à la recherche d'une information. Après lecture sur le sujet, cette approche semble privilégiée pour de nombreux projets pour la raison évoquée.

Un autre exemple sont le fait que nos prefabs ne contiennent que le stricte minimum, et **nos** components ne sont (généralement) pas des dérivés de `MonoBehaviour`. Ils sont tous instanciés dans une classe factory spécifique qui nous permet un controlle plus fin mais surtout moins rigide sur les propriétés de nos entités. Le principal avantage de cette façon de faire est d'éviter de se retrouver à batailler avec le système de *variants* et nested prefabs qui malgré tout reste un outil très haut niveau avec les limitations d'un système haut niveau. De plus cette façon de faire permet de ne pas avoir des tonnes de scripts dans l'inspecteur d'unity. Le revers de la médaille est un gain de complexité pour faire des tests rapides sur les valeurs de certaines propriétés mais rien d'insurmontable.

Les *Scriptable objects* ont étés envisagés mais le temps étant limité nous n'avons pas souhaité creuser cette piste étant confiants dans notre approche.