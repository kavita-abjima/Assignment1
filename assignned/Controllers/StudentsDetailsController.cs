using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.DbContexts;
using StudentsDetails.Dto;
using StudentsDetails.Models;

namespace StudentsDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsDetailsController : ControllerBase
    {
        private readonly studentContext _context;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;


        public StudentsDetailsController(studentContext context, IMapper mapper, IStudentRepository studentRepository)
        {
            _context = context;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        // GET: api/StudentsDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentsDetailDto>>> GetStudentsDetails()
        {
            try
            {
                if (_context.StudentsDetails == null)
                {
                    return NotFound();
                }
                var studentdto = await _studentRepository.GetStudent();
                //var students= _context.StudentsDetails.ToListAsync();

                var studentdto1 = studentdto.Select(x => _mapper.Map<Student>(x));


                return Ok(studentdto1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/StudentsDetails/5
        [HttpGet("{id}")]
        //  public async Task<ActionResult<StudentsDetailDto>> GetStudentsDetail(int id)
        public async Task<ActionResult<StudentsDetailDto>> GetStudentsDetail(int id)
        {
            try
            { 
                if (_context.StudentsDetails == null)
                {
                    return NotFound();
                }
                //var studentsDetail = await _context.StudentsDetails.FindAsync(id);
                var studentsDetail = await _studentRepository.GetStudentById(id);
                if (studentsDetail == null)
                {
                    return NotFound();
                }
                var item = _mapper.Map<StudentsDetailDto>(studentsDetail);
                return item;
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message); 
            }


        }

        // PUT: api/StudentsDetails/5

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentsDetailDto>> PutStudentsDetail(int id, StudentsDetailDto studentsDetail)
        {
            try
            {
                if (id != studentsDetail.Id)
                {
                    return BadRequest();
                }

                var updateStudent = await _studentRepository.UpdateStudentAsync(id, studentsDetail);
                if (updateStudent == null)
                {
                    return NotFound();
                }
                var studentdto1 = _mapper.Map<Student>(updateStudent);
                return Ok(updateStudent);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!StudentsDetailExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //var studentdto1 = _mapper.Map<Student>(updatedStudent);
            //return NoContent();
        }

        // POST: api/StudentsDetails

        [HttpPost]
        public async Task<ActionResult> PostStudentsDetail(StudentsDetailDto student)
        {
            try
            {
                if (_context.StudentsDetails == null)
                {
                    return Problem("Entity set 'studentContext.StudentsDetails'  is null.");
                }
                await _studentRepository.AddStudentAsync(student);
                var studentdto = _mapper.Map<Student>(student);
                //_context.StudentsDetails.Add(studentdto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStudentsDetail", new { id = student.Id }, student);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE: api/StudentsDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentsDetail(int id)
        {
            try
            {
                if (_context.StudentsDetails == null)
                {
                    return NotFound();
                }
                var studentsDetail = await _studentRepository.DeleteStudentAsync(id);
                if (studentsDetail == null)
                {
                    return NotFound();
                }

                //_studentRepository.Remove(studentsDetail);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
           
            
        }

        //[HttpGet("{id}")]
        //private bool StudentsDetailExists(int id)
        //{
        //    return (_context.StudentsDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
