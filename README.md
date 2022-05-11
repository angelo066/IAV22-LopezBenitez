# IAV22-LopezBenitez
This repository is used for the final project of Artificial Intelligence signature.

Propuesta: Inteligencia Artificial que sea capaz de jugar en equipo a tactical shooters, para este proyecto nos centrariamos en reproducir los roles del
tactical shooter multijugador Valorant, desarrollado por Riot Games, estos roles son :

-El controlador: encargado de colocar humos en el mapa para cancelar la visión del enemigo.

-El centinela: encargado de preveer flanqueos del enemigo y de contrarestarlos.

-El duelista: encargado de comprobar ángulos para su equipo y de entrar a las zonas de plante de la bomba.

-El iniciador: encargado de ayudar al duelista en su objetivo.

Para facilitar el trabajo, utilizaremos las habilidades (simplificadas, quitando físicas como la del lanzamiento de una flecha) de los siguientes personajes de
Valorant. 
Controlador : Brimstone.
Centinela : Cypher
Duelista : Jett.
Iniciador: Sova

El objetivo final de la práctica es que los personajes se comuniquen entre si, utilicen sus habilidades de forma coordinada y, si tuviesemos tiempo, implementar
un machine learning en el que los personajes aprendan, donde se colcoan sus enemigos, como juegan sus compañeros, y que tácticas les son más y menos efectivas


## Resumen
La práctica consiste en implementar una inteligencia artificial que sea capaz de trabajar en equipo junto con otras 3, en este repositorio implementaremos la inteligencia artificial
que hará las funciones de iniciador, usando al personaje Sova, del videojuego Valorant. Valorant consiste de dos equipos, el atacante y el defensor, el equipo atacant debe plantar una bomba
al lado de las cajas de radianita(un material valioso que permite generar enegergia rapidamente) para destruirla, mientras que el equipo atacante debe impedirlo. Nosotros nos centraremos en implementar
la coordinación del ataque.

<br>
El entorno de la práctica será el sitio de B del mapa Bind de valorant, modelado en Blender y texturizado.

## Rol en la partida
El iniciador es aquel que debe ayudar a su equipo a entrar al sitio para plantar la bomba, Sova tiene una habilidad llamada Dardo de reconocimiento, esta habilidad es una flecha que al lanzarla se clava a una pared
y revela para Sova y para todos sus compañeros a los enemigos que se encuentren en rango de vision de la flecha.

## Objetivo de la IA
El objetivo de esta IA es que ayude a sus compañeros a revelar a los enemigos, que aprenda que lugares son mejores para lanzar su flecha.

## Punto de partida
Utilizaremos una plantilla de shooter en primera persona para Unity para ahorrarnos el trabajo de implementar las mecánicas básicas de un shooter.

## Pseudocódigo

Utilizaremos el pathFinding de la práctica 3 (El fantasma de la opera) para que los personajes se muevan de un sitio a otro.

Utilizaremos una serie de puntos de influencia para saber que lugares son mejores para lanzar la flecha de reconocimiento.

Utilizarempos un patron de envio de mensajes para conseguir que los personajes se comuniquen entre si.