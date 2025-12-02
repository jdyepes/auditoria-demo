# Audit Demo - Proyecto .NET – Despliegue, Evidencias y Descripción Técnica

---

## 🧱 Estructura del proyecto

AuditDemo.sln
├─ **Auditing.Api/**  
│   └─ API REST principal  
│
├─ **Auditing.Application/**  
│   └─ Casos de uso y lógica de negocio  
│
├─ **Auditing.Domain/**  
│   └─ Entidades y contratos  
│
├─ **Auditing.Infrastructure/**  
│   └─ Persistencia y servicios externos  
│
├─ **Auditing.Tests/**  
│   ├─ Helpers/Repositories/  
│   └─ Pruebas unitarias (xUnit + Moq) organizadas por repositorio  
│
├─ **Auditing.Web/**  
│   └─ Frontend Razor Pages  
│
├─ **db/scripts/**  
│   ├─ 01_create_db.sql  
│   ├─ 02_tables.sql  
│   ├─ 03_functions.sql  
│   ├─ 04_views.sql  
│   └─ 05_seeders.sql  
│
├─ **postman/**  
│   ├─ api_collection.json  
│   └─ api_env.json  
│
└─ **docs/img/**  
    └─ Evidencias visuales
---

## 🚀 Instrucciones de despliegue local por comando

### Backend (.NET 8)
```bash
cd Auditing.Api
dotnet restore
dotnet build
dotnet run

- 

### Razor Pages (.NET 8)
```bash
cd Auditing.Web
dotnet run

- 

## Instrucciones de despliegue local por IDE
- **Backend y Backend:** 
  - Configurar `appsettings.Development.json` con ConnectionStrings.
  - Control + F5 - Establecer como proyecto de inicio el Api y Web.

## Base de datos (SQL Server)
- Ejecutar los scripts en orden desde db/scripts/:
	- 01_create_db.sql
	- 02_tables.sql
	- 03_functions.sql
	- 04_views.sql
	- 05_seeders.sql


## Evidencias de pruebas unitarias
- **Pantallazo IDE:**  
  ![Resultados en IDE](backend/docs/img/test-results-ide.png)
- **Reporte TRX y cobertura:**  
  - TRX: `backend/TestResults/test_results.trx`  
  - Cobertura HTML: `backend/coveragereport/index.htm`  
  - Snapshot: ![Cobertura](backend/docs/img/coverage-report.png)

## Descripción técnica de la solución - Patron DDD
- **Arquitectura:** (Controllers → Services → Repositories/Infrastructure → Domain).  
- **Datos:** integridad referencial; seeds idempotentes.  
- **Pruebas:** xUnit + Moq + FluentAssertions; cobertura mínima.  


## Supuestos realizados

En el desarrollo de la solución se asumieron los siguientes casos de uso básicos:

- **Ver auditoría por filtro**: posibilidad de consultar auditorías aplicando criterios de búsqueda (por fechas, estado, área, etc.).
- **Ver hallazgo**: acceso al detalle de un hallazgo asociado a una auditoría.
- **Ver Responsable**: consulta de la información del responsable (owner) vinculado a la auditoría.
- **Crear responsable**: registro de un nuevo responsable en el sistema.
- **Crear hallazgo**: registro de un nuevo hallazgo asociado a una auditoría existente.
- **Actualizar o subir estatus de hallazgo**: modificación del estado de un hallazgo (ej. de *Pending* a *Completed*).
- **Nota pendiente**: la generación de **reportes** queda como funcionalidad futura a implementar, alguna foreane de relacion entre hallazo y auditoria, DTO en el front, entre otros.

![Cobertura](docs/img/coverage-report.png)

## Flujo de trabajo con Git

Para el desarrollo de esta funcionalidad se siguió un flujo ordenado manualmente basado en Git Flow:

1. **Partida desde `develop`**  
   - Se tomó la rama principal de desarrollo como base.

2. **Creación de rama `feature/audits`**  
   - Se levantó la rama de feature para implementar la plantilla y toda la lógica correspondiente a auditorías, hallazgos y owners.

3. **Implementación de la plantilla y casos de uso**  
   - Se desarrollaron los supuestos definidos (crear/ver auditoría, hallazgo y owner, actualizar estados, etc.).

4. **Pull Request hacia `develop`**  
   - Una vez completada la implementación inicial, se realizó el PR para integrar los cambios en la rama de desarrollo principal.

5. **Continuación en `feature/audits`**  
   - Se mantuvo la rama activa para ajustes posteriores y correcciones, hasta lograr la integración final con `develop`.

6. **Futuro ajuste de Git Flow**  
   - Se prevé un cambio en el flujo para atender nuevos requerimientos:  
     - Creación de nuevas ramas `feature/*` para funcionalidades.  
     - Uso de ramas `fix/*` para correcciones puntuales.  
     - Integración ordenada en `develop` y, eventualmente, en `release` o `main` según corresponda.

---

## Enfoque de arquitectura

El proyecto se construyó **desde cero** partiendo de una solución en blanco con **.NET Core** y **Razor Pages**, aplicando principios de **Domain-Driven Design (DDD)**.

### ¿Por qué usar DDD?
- **Separación clara de responsabilidades**: se distinguen las capas de dominio, aplicación e infraestructura, evitando dependencias innecesarias.
- **Modelo de dominio rico**: las entidades (`Audit`, `Owner`, `Finding`) encapsulan reglas de negocio y garantizan consistencia.
- **Escalabilidad y mantenibilidad**: facilita la evolución del sistema ante nuevos requerimientos sin romper la lógica existente.
- **Lenguaje ubicuo**: el código refleja directamente los conceptos del negocio (auditorías, hallazgos, responsables), lo que mejora la comunicación con stakeholders.
- **Pruebas más robustas**: al tener un dominio bien definido, las pruebas unitarias validan reglas de negocio y no solo detalles técnicos.

📌 Este enfoque asegura que la solución sea **modular, trazable y preparada para crecer**, alineada con buenas prácticas internacionales.


## Colección Postman

La API expuesta con Swagger se importó en Postman.

- Archivo de colección: `docs/postman/Auditing.Api.postman_collection.json`
- Fuente: `https://localhost:5001/swagger/v1/swagger.json`

---

## 📌 Nota final

Este proyecto corresponde a una **prueba técnica desarrollada íntegramente por Jesús Yepes**, aplicando principios de DDD, Git Flow y pruebas unitarias.