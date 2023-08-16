﻿using CarrosAPI.EntityRepository;
using CarrosAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CarrosController(AppDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        [Route("/ListarCarros")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarroModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CarroModel))]
        public IActionResult ListarCarrosEntity()
        {
            try
            {
                var carros = _context.Carros.AsNoTracking().ToList();

                if (carros is null)
                {
                    return NotFound("Carros não encontrados");
                }
                else
                {
                    return Ok(carros);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro ao tratar a sua solicitação");
            } 
        }

        [HttpGet]
        [Route("/ListarCarro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarroModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CarroModel))]
        public IActionResult ListarCarrosEntity(int id)
        {
            try
            {
                var carro = _context.Carros.AsNoTracking().Where(carro => carro.CarroId == id); ;

                if (carro is null)
                {
                    return NotFound("Carro não encontrado");
                }
                else
                {
                    return Ok(carro);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        [Route("/CadastrarCarro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarroModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CarroModel))]
        public IActionResult CadastrarCarro(CarroModel carroModel)
        {
            try
            {
                if (carroModel is null)
                {
                    return BadRequest("Carro nulo");
                }


                Carro carro = new Carro(carroModel?.Modelo??"",carroModel?.Descricao??"",carroModel?.CategoriaId??1);

                _context?.Carros.Add(carro);

                _context?.SaveChanges();

                return Ok(carro);


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut]
        [Route("/AtualizarCarro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarroModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CarroModel))]
        public IActionResult AlterarCarro(int id,Carro carroModel)
        {
            try
            {

                var carro = _context.Carros.AsNoTracking().FirstOrDefault(carro => carro.CarroId == id);

                if (carro is null)
                {
                    return NotFound("Carro não encontrado");
                }
                else
                {
                    carro!.Modelo = carroModel.Modelo;
                    carro.Descricao = carroModel.Descricao;
                    carro.Categoria.CategoriaId = carroModel.CategoriaId;
                    carro.Categoria.NomeCategoria = CarroModel.RetornaNomeCategoria(carroModel.CategoriaId);
                    _context.SaveChanges();
                    return Ok(carro);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

    }
}
