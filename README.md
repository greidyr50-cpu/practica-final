# Sistema de Gestión de Estudiantes

Aplicación de escritorio desarrollada en **C# con Windows Forms** para gestionar la información de estudiantes mediante una lista dinámica en memoria. El proyecto implementa operaciones CRUD (crear, consultar, actualizar y eliminar), validaciones de datos, manejo de excepciones y enumeraciones.

## Integrantes

| Nombre completo             | Matrícula |
| --------------------------- | --------- |
| Braudin Frias               | 2023-3975 |
| Justin Emmanuel Santana Ces | 2023-4538 |
| Moises Alexander Jeffers    | 2023-3238 |
| Greidy Recio                | 2023-3961 |

## Descripción breve

El **Sistema de Gestión de Estudiantes** permite registrar y administrar información académica de estudiantes de forma sencilla mediante una interfaz gráfica. La aplicación utiliza Windows Forms para la interacción con el usuario y una lista dinámica `List<Estudiante>` como almacenamiento temporal durante la ejecución.

El sistema permite consultar los estudiantes registrados, realizar búsquedas, modificar información y eliminar registros con confirmación previa.

## Funcionalidades principales

- Registrar nuevos estudiantes.
- Listar los estudiantes registrados.
- Buscar estudiantes por ID o nombre.
- Actualizar los datos de un estudiante seleccionado.
- Eliminar estudiantes con confirmación.
- Refrescar la información mostrada.
- Validar los datos introducidos.
- Mostrar mensajes de confirmación y error mediante `MessageBox`.
- Manejar errores mediante excepciones.
- Utilizar enumeraciones (`enum`) para valores predefinidos como sexo y estado académico.

## Datos de entrada

La información se introduce mediante los controles del formulario, entre ellos:

- **ID o matrícula**
- **Nombre completo**
- **Edad**
- **Sexo**
- **Carrera**
- **Estado académico**
- Otros datos correspondientes a la información del estudiante.

Los campos de selección, como sexo y estado académico, pueden manejarse mediante controles `ComboBox`.

## Datos que procesa

La aplicación procesa la información utilizando una lista dinámica `List<Estudiante>` durante la ejecución.

Entre los procesos realizados se encuentran:

1. **Validación de datos:** comprobación de campos obligatorios, edad válida e ID no duplicado.
2. **Registro:** incorporación de nuevos estudiantes a la lista.
3. **Consulta:** visualización de los estudiantes en un `DataGridView`.
4. **Búsqueda:** localización de estudiantes por ID o nombre.
5. **Actualización:** modificación de los datos de un estudiante existente.
6. **Eliminación:** eliminación de un estudiante a partir de su registro, previa confirmación.
7. **Manejo de excepciones:** control de errores producidos durante la ejecución mediante estructuras `try/catch/finally`.

## Datos de salida

Los resultados se presentan principalmente mediante:

- **DataGridView:** muestra la lista de estudiantes registrados.
- **MessageBox de confirmación:** solicita confirmación antes de determinadas operaciones, especialmente la eliminación.
- **MessageBox de error:** informa al usuario cuando se produce un dato inválido o una operación no puede realizarse.
- **Mensajes informativos:** notifican el resultado de las operaciones realizadas.

## Estructura y tecnologías

- **Lenguaje:** C#
- **Interfaz gráfica:** Windows Forms
- **Almacenamiento temporal:** `List<Estudiante>`
- **Programación:** Orientada a objetos
- **Operaciones:** CRUD
- **Enumeraciones:** `enum`
- **Validaciones:** datos obligatorios, formato numérico e identificación no duplicada
- **Manejo de errores:** `try/catch/finally`

La organización del proyecto contempla una clase para representar al estudiante y una clase encargada de la gestión de la lista y las operaciones CRUD, manteniendo los formularios enfocados en la interacción con el usuario.

## Capturas de pantalla

### Registrar estudiante

Formulario para registrar un nuevo estudiante, con campos para ID, nombre, edad, sexo (ComboBox), carrera, estado (ComboBox) y fecha.

![Registrar estudiante](<img width="940" height="691" alt="registrar-estudiante" src="https://github.com/user-attachments/assets/7b2f27d4-253b-423d-a86d-5561f333e3bf" />


### Listado de estudiantes

La pantalla de listado permite visualizar los estudiantes registrados mediante un `DataGridView`. También incluye opciones para buscar, refrescar, editar el registro seleccionado y eliminarlo.

![Listado de estudiantes](<img width="940" height="712" alt="listado-estudiantes" src="https://github.com/user-attachments/assets/ce0080ab-6885-4912-ba07-4168b9a8b09c" />


## Requisitos del proyecto

El sistema fue planteado para cumplir con los requisitos del caso práctico:

- Uso de `List<T>` para almacenar estudiantes.
- Implementación completa del CRUD.
- Interfaz gráfica Windows Forms.
- Programación orientada a objetos.
- Uso de al menos dos enumeraciones.
- Validaciones de datos.
- Manejo de excepciones.
- Mensajes claros de confirmación y error.
- Código organizado en métodos con responsabilidades claras.

## Objetivo

Desarrollar una aplicación de escritorio que permita gestionar la información de estudiantes aplicando conceptos fundamentales de programación en C#, incluyendo listas dinámicas, Windows Forms, programación orientada a objetos, validaciones, excepciones, enumeraciones y operaciones CRUD.
