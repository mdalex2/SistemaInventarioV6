﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IAsyncDisposable
    {
        IBodegaRepositorio Bodega { get; }
        Task Guardar();
    }
}
