﻿using ApplicationCore.DTO;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ILivroService
    {
        Task<Livro> AddLivro(Livro livro);
        Task Delete(int LivroID);
        List<Livro> GetAll();
        Task Update(Livro livro);
        Task<Livro> GetByID(int id);
        List<Livro> GetLivrosPorFiltro(LivroDTO filtro);
    }
}

    