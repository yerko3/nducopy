# 📷 Nducopy

**Nducopy** es una herramienta en C# diseñada para optimizar el almacenamiento de tus directorios. El programa analiza carpetas del sistema para detectar imágenes duplicadas comparando exhaustivamente su contenido (no solo el nombre o la extensión), elimina las repeticiones y genera un conjunto de imágenes únicas y limpias.

---

## 🚀 Cómo Funciona y Cómo Probarlo

El proyecto está configurado para trabajar de forma nativa con rutas relativas de prueba. Para ver la aplicación en acción de inmediato, sigue estos pasos:

1. **Preparación de las imágenes:** Coloca tus imágenes (incluyendo los duplicados que quieras testear) dentro de las carpetas locales de prueba llamadas `Prueba1` y `Prueba2`.
2. **Ejecución:** Ejecuta el proyecto. El sistema escaneará ambas carpetas, comparará el contenido real de los archivos y filtrará los duplicados.
3. **Resultado:** En la carpeta de salida `PruebaDestino` se guardará el conjunto final de imágenes totalmente optimizado, sin ninguna repetición.

---

## ⚙️ Configuración de Rutas Personalizadas

Si deseas analizar otras carpetas de tu ordenador en lugar de las carpetas de prueba por defecto, puedes modificar los argumentos de la línea de comandos directamente desde **Visual Studio**:

1. Haz clic derecho sobre el proyecto `ndupcopy` en el *Explorador de soluciones* y selecciona **Propiedades** (o ve al menú superior).
2. En el panel izquierdo, dirígete a la pestaña **Depurar** (Debug).
3. Localiza el campo **Argumentos de la línea de comandos** (Command line arguments).
4. Cambia las rutas relativas actuales por las rutas de las carpetas que deseas escanear y la de destino. 

> 💡 *Ejemplo de la configuración por defecto en Visual Studio 2022:*
> `..\..\..\Prueba1 ..\..\..\Prueba2 ..\..\..\PruebaDestino`

---

## 🛠️ Estructura del Código Principal

El proyecto está organizado de forma modular basándose en sus responsabilidades:

* **`Program.cs`**: El punto de entrada que coordina la lectura de rutas y el flujo principal del programa.
* **`FileReader.cs`**: Se encarga de mapear y acceder de forma segura a los archivos del sistema de almacenamiento.
* **`FileHash.cs`**: Genera identificadores únicos basados en el contenido de la imagen para asegurar una comparación 100% precisa.
* **`CompareFile.cs`**: Lógica algorítmica encargada de contrastar las firmas de los archivos y detectar cuáles están duplicados.
* **`FileCopier.cs`**: Gestiona la transferencia y guardado final de las imágenes únicas hacia la carpeta de destino.
