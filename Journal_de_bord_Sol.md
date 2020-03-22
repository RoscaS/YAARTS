# Sol: Journal de bord

## Semaine du 16 mars

### Objectifs

* Amélioration du débug et des scénarios de tests
* Recherche de solution pour le problème de collisions des flèches (ignore collision global)


### Fait

* Redaction d'une description de l'architecture
* Redaction d'une description de l'IA des entités
* Redaction d'une clarification concernant le projet YARTS (1 "A") fait sur Java l'an passé
* Liste des assets utlisés
* Amélioration du débug et des scénarios de tests

### Reste à faire

* Recherche de solution pour le problème de collisions des flèches (ignore collision global)

### Analyse du problème des flèches

Le problème vient du fait que `Physics.IgnoreCollision` prend deux `collider` et que`Physics.IgnoreLayerCollision` prends deux layers.

Une fleche (prefab appartenant au layer _Projectile_) tirée par une entité de l'équipe A (appartenant au layer _A_) devrait ignorer toutes les entités de l'équipe A et réagir aux collisions de l'équipe B (appartenant au layer _B_).

Comme les deux methodes sont globales, si au temps $t_1$ une entité de l'équipe A fait un call à `IgnoreLayerCollision` avec en argument _Projectile_ et _A_ et qu'au temps $t_2$ une entité de l'équipe B fait un call à `IgnoreLayerCollision` avec en argument _Projectile_ et _B_, toutes les flèches ignoreront toujours toutes les entités appartenant aux layers _A_ et _B_.