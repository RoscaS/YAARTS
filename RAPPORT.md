
- [Introduction](#introduction)
- [Contextualisation](#contextualisation)
  - [Cours de .NET](#cours-de-net)
  - [Cours d'infographie](#cours-dinfographie)
- [Planification](#planification)
  - [Objectifs](#objectifs)
    - [MVP](#mvp)
    - [Utopique](#utopique)
  - [Répartition des tâches](#r%c3%a9partition-des-t%c3%a2ches)
  - [Logistique](#logistique)
    - [Versioning](#versioning)
- [Conception](#conception)
  - [Spécifications](#sp%c3%a9cifications)
    - [Général](#g%c3%a9n%c3%a9ral)
    - [Entités](#entit%c3%a9s)
    - [Récolte](#r%c3%a9colte)
    - [Inputs](#inputs)
    - [Déroulement d'une partie](#d%c3%a9roulement-dune-partie)
  - [Conventions](#conventions)
  - [Techniques de programmation](#techniques-de-programmation)
    - [Extrème programming](#extr%c3%a8me-programming)
    - [Développement itératif](#d%c3%a9veloppement-it%c3%a9ratif)
- [Réalisation](#r%c3%a9alisation)
  - [Architecture](#architecture)
    - [Pattern architectural](#pattern-architectural)
      - [Première itération: Approche programmatique](#premi%c3%a8re-it%c3%a9ration-approche-programmatique)
      - [Seconde itération: Approche prefabs](#seconde-it%c3%a9ration-approche-prefabs)
  - [Mécanismes](#m%c3%a9canismes)
    - [IA](#ia)
      - [Pathfinding](#pathfinding)
      - [Gestion des collisions](#gestion-des-collisions)
      - [États](#%c3%89tats)
    - [Sélection](#s%c3%a9lection)
    - [Brouillard de guerre](#brouillard-de-guerre)
    - [Communication GUI](#communication-gui)
  - [Design](#design)
    - [Entités](#entit%c3%a9s-1)
    - [Animations](#animations)
  - [Interface utilisateur](#interface-utilisateur)
    - [Architecture](#architecture-1)
    - [Panneaux](#panneaux)
      - [Sélection](#s%c3%a9lection-1)
      - [Actions](#actions)
      - [Mini-map](#mini-map)
      - [Ressources](#ressources)
- [Récapitulatif](#r%c3%a9capitulatif)
  - [Objectifs](#objectifs-1)
  - [Bugs](#bugs)
  - [Améliorations](#am%c3%a9liorations)
- [Conclusion](#conclusion)
- [Répartition des tâches](#r%c3%a9partition-des-t%c3%a2ches-1)
  - [Sol Rosca (Architecture)](#sol-rosca-architecture)
  - [Nathan Latino (GUI)](#nathan-latino-gui)
  - [Tristan Seuret (Features)](#tristan-seuret-features)
- [Assets](#assets)
  - [Tools](#tools)
    - [Free](#free)
    - [Payed](#payed)
  - [Assets](#assets-1)
    - [Payed](#payed-1)
- [Références](#r%c3%a9f%c3%a9rences)
  - [Solide](#solide)
  - [Architecture](#architecture-2)
  - [Specific](#specific)
  - [Généraliste](#g%c3%a9n%c3%a9raliste)
  - [Youtube](#youtube)


## Introduction

YAARTS: Yet Again Another Real Time Strategy (Game). Est un prototype de jeu de stratégie en temps réel (Dune 2, Command & Conqueres, Warcraft, Starcraft, ...) réalisé conjointement pour les projets de fin d'année des cours d'infographie et de .NET.

Le terme *jeu de stratégie en temps réel* <st c="rgb">NOTE RTS à partir de mtnt</st> est utilisé pour la première fois en 1992 pour désigner le genre du jeu Dune II basé sur le roman éponyme de Frank Herbert. Depuis, le genre à beaucoup évolué mais principalement graphiquement. En effet, bien que la définition précise fasse l'objet de débats, les RTS sont traditionnellement définis par les termes "récolter", "construire" et "détruire" en plus d'être des jeux où l'action se déroule en temps réel entre les différents participants. En partant des trois termes "récolter", "construire" et "détruire", il est facilement déductible que les intentions des joueurs sont axés autour du fait de "gérer des ressources", "développer une base" et "créer des unités" pour combattre l'adversaire.

Ce projet est en essence inspiré par ce qui a été fait au semestre de printemps 2019 pour le projet P2 Java: YARTS (avec un seul "A", liens en annexe). Ce projet fut une premier tentative d'implémentation d'un RTS. Sans aucune expérience initiale avec la technologie utilisée <st c="rgb">NOTE: LIBGDX</st> et encore moins avec les techniques d'infographie ce projet fut l'occasion de cerner le problème et d'avoir une idée clair de quel sont les principaux obstacles et pièges à éviter. 

YAARTS (avec deux "A") malgré sa dimension supplémentaire a pour but d'aller plus loins que le premier et de proposer un prototype de jeu de stratégie qui implémente toutes les fonctionnalités qui définissent le genre le tout dans un style visuel agréable et cohérent.


## Contextualisation

### Cours de .NET

Un jeu vidéo est un sport complet qui demande de nombreuses qualités sur lesquelles le cours de .NET a mit l'accent. En effet, dans un jeu vidéo, de nombreux composants hétéroclites coexistent et communiquent. Dans ces circonstances et à cette échelle, de nombreux problèmes qui dans le cadre de projets moins vastes n'ont pas le temps de surgir, ont ici tout le loisir de se dévoiler et de bloquer la progression. Pour palier à ça, il est impératif d'observer une certaine rigueur de bout en bout et c'est bien là un des points clé qui est revenu dans toutes les corrections des travaux de ce cours.

Au vue des contraintes temporelles et de l'ambition dans les objectifs, il est nécessaire d'avoir une idée clair de ce que doit faire l'application ainsi que du niveau de finition souhaité. Le premier point permet de réfléchir à une architecture adéquate et le second à définir à partir de quel niveau il est possible de travailler sans plan car les conséquences de code de moins bonne qualité n'aura pas d'impacte car il est en "bout de chaîne" et n'a pas vocation à être étendu. Cette façon de faire permet d'avoir une base solide et robuste qui pourra soutenir du code plus volatile et expérimental ce qui colle bien avec un projet à cheval sur deux matières dont une qui est en pleine phase de découverte.

Le but de la partie C# de ce projet est donc de créer un moteur de jeu de stratégie en temps réel qui puisse servir de terrain d'expérimentation pour la partie infographie du projet. 

### Cours d'infographie

Le projet décrit dans ce rapport étant un jeu vidéo, les liens avec le cours d'infographie sont nombreux. De façon générale, les notions du premier semestre sont la base de culture générale nécessaire à pouvoir naviguer sereinement dans les problèmes plus haut niveau que pose Unity. La matière du second semestre quant à elle est l'aperçu nécessaire pour pouvoir entamer un projet plus conséquent que quelques scripts qui se courent après.

Même si la partie purement technique du pipeline OpenGL n'a pas été directement utilisée, les notions liées aux *shaders* et leurs déclinaisons *vertex* et *fragments* sont au coeur du développement de jeux vidéos. Malgré leur caractère bas niveau, ces notions reviennent dans de nombreuses discussions qui touchent des aspects plus haut niveau et dont il ne serait pas possible de saisir la pertinence ou les nuances sans avoir une idée du contexte dans lequel elles s'inscrivent.

Les notions de scène et de caméra sont fondamentales et reviennent systématiquement. Le fait de bien comprendre la notion de repère est un gain cognitif indispensable pour comprendre de nombreuses explications où les conversions sont faites implicitement en assumant que le lecteur jongle avec ces notions. De la même façon, une bonne compréhension des projection et de leur spécificités ainsi que la capacité de visuellement les différencier sont autant de place mentale libérée pour pouvoir résoudre d'autres problèmes qui assument que ces notions sont acquises.

Au travers du projet dont fait état le présent rapport, une bonne partie des notions du premier semestre ont été touchées mais au travers du prisme d'Unity qui apporte une vision considérablement plus haut niveau. Cette perspective nouvelle apporte des explications sous la forme de complément là ou les questions n'était pas forcément évidentes à formuler dans le contexte bas niveau du premier semestre. Typiquement, de part leur simplicité à mettre en place dans Unity, les différents types d'éclairages, leurs attributs et leur relation avec la physique permet de gagner un niveau d'abstraction qui automatiquement fait assimiler un certain nombre de concepts plus basiques. De même, les outils que propose Unity pour visualiser et jouer avec les matériaux, les modèles et les textures permet d'encrer les notions d'UV <st c="rgb">NOTE: Coordonnées de textures dans le cours</st>, de normales, les déformations causées par les application de textures et de leur comportement vis-à-vis de la lumière.

Autrement dit, de façon plus pragmatique, le cours d'infographie donne une vision suffisamment globale de la matière pour permettre entre autres de ne pas perdre de temps à lire un article sur la gestion des ombres dans l'herbe via programmation de shader alors que ce qui est recherché est le type d'éclairage qui serait le plus pertinent pour une scène.

## Planification

### Objectifs

#### MVP

* Une carte ("plateau de jeu") sur lequel se passe l'action (prototype)
* Deux factions symétriques
* Entités:
    * Batiments de production
    * Unités de production/récolte
    * Unités offensives
* IA des entités:
    * Pathfinding
    * Collision avoidance (gestion des obstacles mobiles dans le pathfinding)
    * Etats: Idle, moving, engaging, engaged, dead en fonction du contexte
    * Gestion de la ligne de vue et de la portée
* Mecanisme de selection des entités (simple et multiple)
* Gestion des ressources
* Brouillard de guère
* GUI (prototype)
    * Menu principale
    * Une interface utilisateur qui contient les éléments suivants:
        * Une mini carte
        * Affichage des ressources
        * Affichage de la population
        * Panneau de selection
        * Panneau des actions de la selection
        * Curseur intelligent dont la forme dépend du contexte

#### Utopique

* IA qui joue l'adversaire
* Diversifications des cartes de jeu
* Génération procédurale de cartes
* Éléments de gameplay (technoogies, amélioration des unités, variété dans les unités)
* GUI moins prototype

### Répartition des tâches

### Logistique

#### Versioning

## Conception

### Spécifications

#### Général

* **Plateforme**: PC
* **Os**: Windows & Linux
* **Langage**: C#
* **Moteur de rendu**: Unity3D
* **Genre**: Stratégie
* **Sous-genre**: Temps réel
* **Perspective**: 3D
* **Camera**: Perspective
* **Contrôle**: Clavier + souris
* **Type de joueurs**: 
    * Humain (`Player`)
    * Ordinateur (`CPU`)
* **Contexte scénaristique**: 
  * Médiéval 
  * Toon
* **Économie**: 
    * Récolte de ressources


#### Entités

Les entités sont de trois types et sont toutes de la classe `Entity`. C'est leurs combinaisons spécifiques de *components* qui font leurs particularités:

* **Character**
* **Structure**
* **Collectible**





#### Récolte
La ressource récoltable est le nerf de la guerre. Elle se trouve en quantité limité sur la carte dans des cellules contigues dont l'affichage reflète cette état.

Ces cellules possède un certain nombre de points de ressource et sont épuisables. Un clic sur la cellule permet d'avoir des information sur sa quantité de ressouce. 

Ces ceullues peuvent être exploitées par une **unité utilitaire** qui peut transporter un nombre finit de ressource. Chaque unité de temps $t$ une resource est transférée de la cellule à l'unité utilitaire. Une fois plein, l'unité utilitaire retourne automatiquement au bâtiment principal (base) et les ressources qu'elle contient sont transférées au pool de ressource du joueur. 

Pour initier ce méchanisme, le joueur doit sélectionner une ou plusieurs unitées utilitaires et cliquer droit sur une cellule contenant des ressources.

Ce méchanisme se poursuit tant que le joueur ne sélectionne pas une des unité utilitaire à la tache et ne la déplace sur une cellule sans ressource.

Une fois la ressource épuisee, la cellule devient une cellule vide (sa texture change en conséquant).

#### Inputs

* Sélections:
* clic gauche sur une entité permet d'afficher des informations la concernant.
* clic gauche maintenu permet de faire un rectangle de sélection qui sélectionne plusieurs entités mobiles crées par le joueur.
* clic droit sur une entité sans sélection préalable ne fait rien.
* clic droit sur une entité avec une sélection:
    * si l'entité possède des points de vie et n'est pas de l'équipe du joueur, donne l'ordre d'attaquer.
    * si l'entité est amie, elle s'y rend.
    * si l'entité est un élément de décor, ne fait rien.
* clics droit sur une cellule vide avec une sélection:
    * la sélection s'y rend.
* Déplacement de la camera:
  * wasd 
  * pression sur le bouton central de la souris


#### Déroulement d'une partie

Cette partie décrit le scénario final 

Au début d'une partie, le joueur se retrouve au commandement d'un bâtiment principale ainsi que une petite troupe ($n$ à définir) d'unitées utilitaires. Un certain nombre de crédit (ressource) lui sont alloués. Le bâtiment principal permet de produire de nouvelles unitées utilitaires qui elles mêmes peuvent construire des bâtiments de production d'unitées offensives ou des bâtiments utilitaires pour augmenter la population. Le bâtiment principal offre une certaine limite de population qu'il est nécessaire de faire augmenter au fur et à mesure de la production d'unitées. Cette augmentation de la population se fait par la construction de nouveau bâtiments utilitaires ("maisons").

Pour assurer sa pérénité, il est nécessaire que le joueur investisse des unitées utilitaires dans la récolte de ressources qu'il investira dans de nouvelles unitées utilitaires ou des bâtiments de production d'unitées offensives pour au final amasser une armée suffisante pour détruire le joueur advèrse.

### Conventions

* Le code suit la convention K&R.
* Les noms du code sont écrits en anglais.
* Les distances se mesurent en pixels et l'origine est en bas à gauche

### Techniques de programmation

#### Extrème programming

#### Développement itératif

## Réalisation

### Architecture

Une longue discussion à été mise en place au sujet de l'architecture. Initialement on hésitait très fortement pour partir sur unity DOTS et un pattern architectural de type ECS. Mais finalement, suite à la lecture de nombreuses ressources et entre autres l'excellent livre "Game Programming Patterns" de Robert Nystrom, nous avons décidé d'épouser une architecture de type "Component" qui est l'architecture qu'utilise Unity pour ses GameObjects. Nos entités seront des conteneurs à components.

#### Pattern architectural

##### Première itération: Approche programmatique
Le diagramme suivant présente l'architecture qui a finalement été décidée:

![](https://i.imgur.com/FGRo4Vh.png)

Les classes ne sont pas complètes et ne présentent que les membres importants pour permettre le travail en groupe.

Nous avons ici le coeur de la logique. La partie inputs et ses différents niveaux d'abstraction sur la gauche, au centre le système de sélection et d'entités qui sont intimement liés par un pattern Composit. Les `Entity` elles-mêmes qui utilisent le pattern Component (Pattern plus spécifique aux jeux vidéos qui ne fait pas partie des patterns du gang of 4).

Nous n'avons que peu d'expérience sur la partie GUI de part nos prototypes et il nous est présentement impossible de réelement l'architecturer. Donc nous adoptons une stratégie qui consiste à l'avoir en tant que module faiblement couplé et de tatonner. Une fois les principales difficultés ciblées, une discussion est prévue afin de mettre en place un refactor de cette portion du code pour mettre en place une architecture plus extensible

La classe `Entity` comporte des membres (Components) dont le type de retour est suffixé d'un `?` cela veut dire que ces membres peuvent être null. Les différents components sont sur la droite du diagramme.

La communication se fait principalement par le biais d'un pattern Observer pour réduire le couplage et favoriser l'ajout de nouvelles fonctionnalités.

##### Seconde itération: Approche prefabs

### Mécanismes

#### IA

##### Pathfinding

##### Gestion des collisions

##### États

Les entités aussi bien statiques que mobiles ont leur état régi par la machine d'état suivante:

![](https://i.imgur.com/fKvpIJg.png)

Cette machine d'état pilote entre autres le component `Animation` qui est en charge de la communication avec le système *Animator* d'Unity. Cette façon de faire nous permet d'éviter certaines limitations d'*Animator*.

De façon général, une approche code est privilégiée.

Par exemple, c'est notre code qui pilote Animator et non l'inverse, nous sommes d'avis qu'il est plus facile d'avoir toute l'information sous les yeux sans devoir cliquer à gauche et à droite dans l'UI d'Unity à la recherche d'une information. Après lecture sur le sujet, cette approche semble privilégiée pour de nombreux projets pour la raison évoquée.

Un autre exemple sont le fait que nos prefabs ne contiennent que le stricte minimum, et **nos** components ne sont (généralement) pas des dérivés de `MonoBehaviour`. Ils sont tous instanciés dans une classe factory spécifique qui nous permet un controlle plus fin mais surtout moins rigide sur les propriétés de nos entités. Le principal avantage de cette façon de faire est d'éviter de se retrouver à batailler avec le système de *variants* et nested prefabs qui malgré tout reste un outil très haut niveau avec les limitations d'un système haut niveau. De plus cette façon de faire permet de ne pas avoir des tonnes de scripts dans l'inspecteur d'unity. Le revers de la médaille est un gain de complexité pour faire des tests rapides sur les valeurs de certaines propriétés mais rien d'insurmontable.

Les *Scriptable objects* ont étés envisagés mais le temps étant limité nous n'avons pas souhaité creuser cette piste étant confiants dans notre approche.

#### Sélection

#### Brouillard de guerre

#### Communication GUI

### Design

#### Entités

#### Animations

### Interface utilisateur

#### Architecture

#### Panneaux

##### Sélection

##### Actions

##### Mini-map

##### Ressources

## Récapitulatif

### Objectifs

### Bugs

### Améliorations

## Conclusion


## Répartition des tâches

### Sol Rosca (Architecture)
* Architecture générale: **ok**
* Architecture des entités: **ok**
* Mécanisme de sélection: **ok**
* IA des entités
* Unités offensives
* Gestion des ressources
* Unité de prod/récolte
* Batiment de production
* Communication moteur-gui

### Nathan Latino (GUI)
* Menu principal
* Mini-carte
* Affichage des ressources
* Affichage de la population
* Panneau de sélection: **ok**
* Panneau d'action de la sélection: **ok**

### Tristan Seuret (Features)
* Fog Of War
* Curseur intelligent dont la forme dépend du contexte si possible



## Assets

Le fait est que nous aimons Unity et nous avons investi dans certains tools et packs d'assets.

### Tools

#### Free
* Debug Drawing
* LiteFPSCounter
* Icon Maker
* ProBuilder
* ProGrid
* PolyBrush

#### Payed
* Peak
* Console Pro
* QHierarchy
* Rainbow Folders

### Assets

#### Payed
* ToonyTinyPeople
* Tarbo-FantasyVillage
* Polygonal Arsenal



## Références

### Solide
* [Rts group movement](https://sandruski.github.io/RTS-Group-Movement/)
* [Fog of war](https://andrewhungblog.wordpress.com/2018/06/23/implementing-fog-of-war-in-unity/)
* [Optimisation](https://www.habrador.com/tutorials/unity-optimization/)
* [Profiler](https://makaka.org/unity-tutorials/optimization)
* [UI System architecture](https://www.gamasutra.com/blogs/YankoOliveira/20180108/312617/A_UI_System_Architecture_and_Workflow_for_Unity.php)
* [ECS Deep Dive](https://rams3s.github.io/blog/2019-01-09-ecs-deep-dive/)
* [ECS & Job System](https://www.reddit.com/r/Unity3D/comments/9mxyrk/unity_ecs_and_job_system_in_production/)
* [AI Navigation - unity cookbook](https://books.google.ch/books?id=UMhsDwAAQBAJ&pg=PA603&lpg=PA603&dq=unity+navmesh+steering&source=bl&ots=hvvr3CJGkI&sig=ACfU3U2-SQqlOh2z3ZdBIRgFJl_NX6XU5A&hl=fr&sa=X&ved=2ahUKEwjdtYfF5YjoAhWByqQKHU7FAqM4ChDoATAHegQIBRAB#v=onepage&q=unity%20navmesh%20steering&f=false)

### Architecture

* [Game Programming patterns](http://gameprogrammingpatterns.com/)
* [Design patterns in Unity](https://www.patrykgalach.com/2019/05/06/design-patterns-in-unity/?cn-reloaded=1)
* [Message bus pattern](https://github.com/franciscotufro/message-bus-pattern)
* [Messaging Architecture in Unity](https://medium.com/@tkomarnicki/messaging-architecture-in-unity-6e6409bdda02)
* [Unity events vs C# events](https://itchyowl.com/events-in-unity/)
* [Refactoring.guru](https://refactoring.guru/design-patterns)


### Specific

* [URP light shadows](https://answers.unity.com/questions/1690727/universal-render-pipeline-point-light-shadows.html)
* [Ballistics](https://gamedev.stackexchange.com/questions/71392/how-do-i-determine-a-good-path-for-2d-artillery-projectiles/71440#71440)

### Généraliste

* [Habrador: tutos Unity](https://www.habrador.com/)

### Youtube

* [GameGrind - Enemy Attack and aggro](https://www.youtube.com/watch?v=Bs0rJEkYBvc)
* [Quill18reates - Sélection](https://www.youtube.com/watch?v=OOkVADKo0IM)
* [Unity DOTS tutorial (fr)](https://www.youtube.com/watch?v=Y33Hc9raS_Y&list=PLVko8yLzEsQJStJ15W5rYQEuEY9erJqIr&index=8)
* [Unity DOTS CodeMonkey](https://www.youtube.com/playlist?list=PLzDRvYVwl53s40yP5RQXitbT--IRcHqba)
* [DOTS 0.4.0](https://www.youtube.com/watch?v=aw4AkdJIF2k&list=PLS6sInD7ThM3L4AtxjixzA-3vRUjAssa4&index=4)