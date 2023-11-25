# tl2-tp10-2023-juanigramajo

---

## Detalles punto 3
- Si el usuario no es administrador, los botones de editar y eliminar no funcionan
    - el editar redirecciona al listado.
    - el eliminar pregunta si esta seguro pero no elimina nada.
- Se podrían ocultar esos botones
- Mostrar mensaje de loggearse o no tener permisos al intentar acceder a un view.

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- LoginController/loggearUsuario sobre almacenar en sesión la contraseña
- Por qué en el Views/Login/@model tengo que mandarle una ruta específica y los demas models no