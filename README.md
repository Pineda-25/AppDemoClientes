# AppDemoClientes

Aplicación de escritorio desarrollada en **C# (.NET 10 - Windows Forms)** bajo una **arquitectura desacoplada en 4 capas** (`Entidades`, `DAL`, `BOL`, `UI`) para la gestión integral de **Clientes** y **Proveedores**.

---

> [!IMPORTANT]
> ### ⚠️ REQUISITO OBLIGATORIO DE FUNCIONAMIENTO
> Esta aplicación de escritorio **NO se conecta directamente a SQL Server**. Toda la persistencia, autenticación, validación de duplicados y consultas a la base de datos se realizan consumiendo el backend **[`ClientesApi`](https://github.com/Pineda-25/ClientesApi)**.
>
> **SI EL SERVICIO BACKEND `ClientesApi` NO ESTÁ INICIADO, LA APLICACIÓN NO FUNCIONARÁ:**
> - No se mostrarán registros en las tablas (`DataGridView`).
> - Las operaciones de búsqueda, registro, edición y eliminación arrojarán un error: *"No hay comunicación con el servidor"*.
> - Para que `AppDemoClientes` funcione correctamente, el proyecto **`ClientesApi` debe estar ejecutándose en segundo plano** escuchando en la URL base: `https://localhost:7177/`.

---

## Tabla de Contenidos
- [Dependencia Obligatoria: ClientesApi](#dependencia-obligatoria-clientesapi)
- [Arquitectura de la Solución](#arquitectura-de-la-solucion)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Módulos del Sistema](#modulos-del-sistema)
- [Reglas de Negocio y Validaciones](#reglas-de-negocio-y-validaciones)
- [Flujo de Comunicación HTTP](#flujo-de-comunicacion-http)
- [Instrucciones de Puesta en Marcha](#instrucciones-de-puesta-en-marcha)

---

## Dependencia Obligatoria: ClientesApi

Para operar el sistema, debes clonar e iniciar el repositorio de la API:

* **Repositorio Backend:** [https://github.com/Pineda-25/ClientesApi.git](https://github.com/Pineda-25/ClientesApi)
* **Endpoints consumidos por esta app:**
  * `api/clientes` (CRUD de clientes y búsqueda con filtro)
  * `api/proveedores` (CRUD de proveedores y validación de RUC)

---

## Arquitectura de la Solución

El proyecto sigue una arquitectura desacoplada en capas basada en el principio de responsabilidad única (SRP):

```
┌─────────────────────────────────────────────────────────────┐
│                    Capa UI (Windows Forms)                  │
│       FrmMain  ──►  FrmCliente  ──►  FrmProveedor           │
└──────────────────────────────┬──────────────────────────────┘
                               │
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                  Capa BOL (Business Object Layer)            │
│            ClienteBol         │        ProveedorBol         │
│          Reglas de Negocio, Normalización y Validaciones    │
└──────────────────────────────┬──────────────────────────────┘
                               │
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                 Capa DAL (Data Access Layer)                │
│       IClienteDal / ClienteDal   │  IProveedorDal / ...     │
│             Consumo HTTP REST mediante HttpClient           │
└──────────────────────────────┬──────────────────────────────┘
                               │
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                 Capa Entidades (Dominio / Modelos)          │
│               Cliente        │       Proveedor              │
└─────────────────────────────────────────────────────────────┘
                               ▲
                               │  (Peticiones HTTP JSON)
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                     BACKEND RESTful API                     │
│               Repositorio: ClientesApi                      │
│               Puerto: https://localhost:7177/               │
│               Motor: ASP.NET Core (.NET 10) + SQL Server    │
└─────────────────────────────────────────────────────────────┘
```

1. **Entidades:** Clases POCO (`Cliente`, `Proveedor`) compartidas entre todas las capas.
2. **DAL (Data Access Layer):** Abstracción mediante interfaces (`IClienteDal`, `IProveedorDal`) y llamadas HTTP asíncronas (`HttpClient`, `PutAsJsonAsync`, `PostAsJsonAsync`, `GetAsync`, `DeleteAsync`).
3. **BOL (Business Object Layer):** Validación estricta de datos (longitudes, regex de DNI/RUC), normalización de cadenas (`Trim`) y lógica previa al envío a la API.
4. **UI (User Interface):** Formularios Windows Forms (`FrmMain`, `FrmCliente`, `FrmProveedor`) con eventos asíncronos (`async/await`) y retroalimentación visual al usuario.

---

## Estructura del Proyecto

```text
AppDemoClientes/
├── AppDemoClientes.slnx             # Solución .NET
├── Entidades/                       # Capa de Modelos de Dominio
│   ├── Cliente.cs                  # Entidad Cliente (Id, Apellidos, Nombres, DNI, etc.)
│   └── Proveedor.cs                # Entidad Proveedor (Id, RazonSocial, RUC, Telefono, Correo)
├── DAL/                             # Capa de Acceso a Datos (REST)
│   ├── ApiException.cs             # Excepción personalizada de respuesta HTTP
│   ├── IClienteDal.cs              # Interfaz de persistencia para clientes
│   ├── ClienteDal.cs               # Implementación cliente REST para clientes
│   ├── IProveedorDal.cs            # Interfaz de persistencia para proveedores
│   └── ProveedorDal.cs             # Implementación cliente REST para proveedores
├── BOL/                             # Capa de Lógica de Negocio
│   ├── ReglaNegocioException.cs    # Excepción para control de reglas de negocio
│   ├── ClienteBol.cs               # Reglas y validaciones de clientes
│   └── ProveedorBol.cs             # Reglas y validaciones de proveedores
└── UI/                              # Capa de Presentación
    ├── Program.cs                  # Inyección de dependencias y arranque
    ├── FrmMain.cs                  # Menú principal de selección
    ├── FrmCliente.cs               # Mantenimiento CRUD de clientes
    └── FrmProveedor.cs             # Mantenimiento CRUD de proveedores
```

---

## Módulos del Sistema

### 1. Menú Principal (`FrmMain`)
Ventana inicial sobria y compacta para seleccionar el módulo de trabajo:
- **Botón Clientes:** Abre el formulario [`FrmCliente`](file:///C:/Users/SENATI/source/repos/AppDemoClientes/UI/FrmCliente.cs).
- **Botón Proveedores:** Abre el formulario [`FrmProveedor`](file:///C:/Users/SENATI/source/repos/AppDemoClientes/UI/FrmProveedor.cs).

### 2. Mantenimiento de Clientes (`FrmCliente`)
- Búsqueda dinámica por nombres, apellidos o DNI (con tecla Enter o botón Buscar).
- Cuadrícula `DataGridView` con selección de fila completa y filas alternadas.
- Controles de captura para Apellidos, Nombres, Correo, DNI, Teléfono y Dirección.
- Validación de solo dígitos en DNI y teléfono (`SoloDigitos`).
- Botones de acción: **Nuevo**, **Guardar** (crear/actualizar) y **Eliminar**.

### 3. Mantenimiento de Proveedores (`FrmProveedor`)
- Búsqueda en tiempo real por Razón Social o RUC.
- Visualización tabular con columnas formateadas para gestión comercial.
- Captura de Razón Social, RUC (11 dígitos numéricos), Teléfono y Correo.
- Control de unicidad de RUC gestionado por la API REST.

---

## Reglas de Negocio y Validaciones

- **Clientes:**
  - Apellidos y Nombres: Obligatorios y no vacíos.
  - DNI: Exactamente 8 dígitos numéricos con validación regex (`^\d{8}$`).
  - Teléfono: 9 dígitos numéricos obligatorios.
- **Proveedores:**
  - Razón Social: Obligatoria y no vacía.
  - RUC: Exactamente 11 dígitos numéricos con validación regex (`^\d{11}$`).
  - Teléfono: Si se ingresa, debe contener caracteres numéricos válidos.

---

## Flujo de Comunicación HTTP

```text
UI (Evento) ──► BOL (Validación) ──► DAL (JSON Request) ──► ClientesApi (https://localhost:7177)
    ▲                                                                 │
    └────────────── Actualiza Grid ◄────── JSON Response ─────────────┘
```

---

## Instrucciones de Puesta en Marcha

Para ejecutar el ecosistema completo correctamente:

### Paso 1: Iniciar el backend `ClientesApi`
```bash
git clone https://github.com/Pineda-25/ClientesApi.git
cd ClientesApi
dotnet run --project ClientesApi/ClientesApi.csproj
```
*(Verifica que indique `Now listening on: https://localhost:7177`)*.

### Paso 2: Ejecutar `AppDemoClientes`
1. Abrir la solución **`AppDemoClientes.slnx`** en Visual Studio.
2. Compilar la solución con **`Ctrl + Mayús + B`**.
3. Presionar **`F5`** (o clic en Iniciar) para ejecutar el menú principal `FrmMain`.
