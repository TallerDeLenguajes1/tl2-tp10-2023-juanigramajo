# tl2-tp10-2023-juanigramajo

---

## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Si el usuario es administrador, no puede cambiar su propio rol a operador
- Si el usuario no tiene permisos no puede crear usuarios ni tareas, notificar eso con alerta
- Si no esta loggeado ofrezco para crear un usuario, pero si no esta loggeado no puede crear tampoco ajsj corregir

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- LoginController/loggearUsuario sobre almacenar en sesión la contraseña
- Por qué en el Views/Login/@model tengo que mandarle una ruta específica y los demas models no
- Sobre el viewBag, como se implementaría con otro viewModel