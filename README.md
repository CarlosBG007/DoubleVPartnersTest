# DoubleVPartnersTest
Repository created for hosting test source code

La solución contiene 2 proyectos:

1) TicketManagement: el cual es la API creada con .NET Core 3.1 y tiene un controllador "TicketController" encargado de exponer metodos que a su vez llaman a otros metodos de la clase "TicketService". Para efectos de la prueba, hago uso de una base de datos en memoria para poder crear, eliminar, editar y recuperar tickets.

2) Ticket.Tests: el proyecto de pruebas unitarias, lo hice con el framework NUnit y tiene una clase "TicketServiceTests" la cual contiene los metodos para realizar las pruebas unitarias

3) Ejecutar el proyecto "TicketManagement", los metodos estan expuestos en:
  "http://localhost:5000/api/Ticket"
 
 Puede que de su lado el numero de puerto sea distinto, pero el resto de la URL es igual como esta descrito.
 En mi caso yo usé Postman para probar mis metodos, pueden usar otra herramienta de su elección y verificar el funcionamiento
