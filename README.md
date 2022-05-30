Actividad 4 - Uso de sistemas de control de versiones





Ingeniería de Software.
mantenimiento de software
francisco castro


By.

Maricio Bello  

Jhonny Alejandro Sánchez Q




28 mayo 2022





JUSTIFICACIÓN

El rápido avance de las nuevas tecnologías las cuales fomentaron la innovación y el interés por consolidar competitivo, ha llevado progresivamente al desarrollo de nuevas formas de recopilar datos, de una manera más precisa y eficiente, obligando a las pequeñas, medianas y grandes empresas, a contratar o crear soluciones informáticas personalizadas que se ajusten a su modelo de negocio, permitiendo así mejorar sus procesos internos, satisfacer las necesidades de sus clientes y obtener un gran incremento productivo y económico.



PLANTEAMIENTO DEL PROBLEMA

caso
Sistemas Infinitos SAS tiene una campaña telefónica nueva del cliente “SALONES EMPRESARIALES SA”;
El Cliente requiere en atender llamadas de “clientes” potenciales de “SALONES EMPRESARIALES SA” en las que desean información para reservar salones para eventos de sus empresas.
Se requiere hacer una aplicación que será utilizada por un agente de Call Center, para capturar la solicitud de información de salones del centro: “SALONES EMPRESARIALES XYZ”.


Requerimientos funcionales (historias de usuarios)

Capturar datos del cliente
Información general del evento
Construir una base de datos relacional que soporte la aplicación para la campaña de “SALONES EMPRESARIALES SA
Construir una aplicación web que debe tener un formulario para crear y actualizar clientes con datos como:
una. identificacion
b. Nombre y apellido
C. telefono
d. correo
mi. Departamento (Lista Desplegable)
F. Ciudad (Lista Desplegable Dependiente al departamento)
gramo. Rango de Edad (Lista Desplegable)

La aplicación debe tener una opción o formulario para crear y eliminar una solicitud de salones asociados a un cliente; la solicitud debe tener
una. Quien realiza la reserva
b. Fecha de evento
C. Cantidad de personas
d. Motivo (lista desplegable de: Evento empresarial, despedida
empresa, desayuno comercial, almuerzo)
mi. Observaciones
F. Estado (Confirmado, No confirmado)
Construir una vista en base de datos y web que permita ver información de las solicitudes confirmadas realizadas en un rango de fechas seleccionadas

Se debe construir un Servicio Web Api Rest para realizar cualquier acción (CRUD).

En la aplicación NO es necesario validar la disponibilidad de salones (esto es un proceso posterior en el (Call center), simplemente registrar la solicitud de salones

DESCRIPCION DE LA ARQUITECTURA
Arquitectura Cliente Servidor

Enlace Prototipo: SalonesAPP
La arquitectura cliente servidor consiste básicamente en un Servicio disponible y un Cliente que consume dicho servicio, de allí el terminal Cliente Servidor.
