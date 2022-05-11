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
Centinela : Cypher.
Duelista : Jett.
Iniciador: Sova.

El objetivo final de la práctica es que los personajes se comuniquen entre si, utilicen sus habilidades de forma coordinada y, si tuviesemos tiempo, implementar
un machine learning en el que los personajes aprendan, donde se colcoan sus enemigos, como juegan sus compañeros, y que tácticas les son más y menos efectivas


## Resumen
La práctica consiste en implementar una inteligencia artificial que sea capaz de trabajar en equipo junto con otras 3. 

En este repositorio implementaremos la inteligencia artificial que hará las funciones de iniciador, usando al personaje Sova, del videojuego Valorant. 

Valorant consiste de dos equipos, el atacante y el defensor, el equipo atacante debe plantar una bomba
al lado de las cajas de radianita (un material valioso que permite generar enegergia rápidamente) para destruirla, mientras que el equipo atacante debe impedirlo. Nosotros nos centraremos en implementar
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

function pathfindAStar(graph: Graph,
	 start: Node,
	 end: Node,
	 heuristic: Heuristic
	 ) -> Connection[]:
	 # This structure is used to keep track of the
	 # information we need for each node.
	 class NodeRecord:
	 node: Node
	 connection: Connection
	 costSoFar: float
	 estimatedTotalCost: float

	 # Initialize the record for the start node.
	 startRecord = new NodeRecord()
	 startRecord.node = start
	 startRecord.connection = null
	 startRecord.costSoFar = 0
	 startRecord.estimatedTotalCost = heuristic.estimate(start)

	 # Initialize the open and closed lists.
	 open = new PathfindingList()

	 open += startRecord
	 closed = new PathfindingList()

	 # Iterate through processing each node.
	 while length(open) > 0:
		 # Find the smallest element in the open list (using the
		 # estimatedTotalCost).
		 current = open.smallestElement()

		 # If it is the goal node, then terminate.
		 if current.node == goal:
		 	break

		 # Otherwise get its outgoing connections.
		 connections = graph.getConnections(current)

		 # Loop through each connection in turn.
		 for connection in connections:
			 # Get the cost estimate for the end node.
			 endNode = connection.getToNode()
			 endNodeCost = current.costSoFar + connection.getCost()

		 # If the node is closed we may have to skip, or remove it
		 # from the closed list.
		 if closed.contains(endNode):
			 # Here we find the record in the closed list
			 # corresponding to the endNode.
			 endNodeRecord = closed.find(endNode)

		 # If we didn’t find a shorter route, skip.
		 if endNodeRecord.costSoFar <= endNodeCost:
		 	continue

			 # Otherwise remove it from the closed list.
			 closed -= endNodeRecord

			 # We can use the node’s old cost values to calculate
			 # its heuristic without calling the possibly expensive
			 # heuristic function.
			 endNodeHeuristic = endNodeRecord.estimatedTotalCost -
			 endNodeRecord.costSoFar

			 # Skip if the node is open and we’ve not found a better
			 # route.
		 else if open.contains(endNode):
			 # Here we find the record in the open list
			 # corresponding to the endNode.

		 	endNodeRecord = open.find(endNode)

		  # If our route is no better, then skip.
		  if endNodeRecord.costSoFar <= endNodeCost:
		  	continue

			  # Again, we can calculate its heuristic.
			  endNodeHeuristic = endNodeRecord.cost -
			  endNodeRecord.costSoFar

			  # Otherwise we know we’ve got an unvisited node, so make a
			  # record for it.
		  else:
			  endNodeRecord = new NodeRecord()
			  endNodeRecord.node = endNode

			  # We’ll need to calculate the heuristic value using
			  # the function, since we don’t have an existing record
			  # to use.
			  endNodeHeuristic = heuristic.estimate(endNode)

			  # We’re here if we need to update the node. Update the
			  # cost, estimate and connection.
			  endNodeRecord.cost = endNodeCost
			  endNodeRecord.connection = connection
			  endNodeRecord.estimatedTotalCost = endNodeCost +
			 endNodeHeuristic

		 # And add it to the open list.
		 if not open.contains(endNode):
			 open += endNodeRecord

		 # We’ve finished looking at the connections for the current
		 # node, so add it to the closed list and remove it from the
		 # open list.
		 open -= current
		 closed += current

	 # We’re here if we’ve either found the goal, or if we’ve no more
	 # nodes to search, find which.
	 if current.node != goal:
	 # We’ve run out of nodes without finding the goal, so there’s
	 # no solution.
	 return null

	 else:
	 # Compile the list of connections in the path.

	 path = []

	 # Work back along the path, accumulating connections.
	 while current.node != start:
	 path += current.connection
	 current = current.connection.getFromNode()

	 # Reverse the path, and return it.
	 return reverse(path)

Utilizaremos una serie de puntos de influencia para saber que lugares son mejores para lanzar la flecha de reconocimiento.

#When arrow collides
onArrowCollision(){
	create RigidBody(radius = arrowRadius)
}

#Loading hot spots
hotSpots = previousHotSpots;

#Area that detects enemies
onAreaCollision(collision col){
	if (col.getComponent<hotSpot>()) {
		hotSpots.add(col.gameObject);
		sendMessage(enemyDetected);
	}
}


Utilizarempos un patron de envio de mensajes para conseguir que los personajes se comuniquen entre si.

#Sending messages
send(msg type){
	sendEnemyPosition(); // for example
}
#Recieving messages
recieve(msg type){
	if (enemyPositionType)
	{
		lookat(enemyPosition);
	}
}