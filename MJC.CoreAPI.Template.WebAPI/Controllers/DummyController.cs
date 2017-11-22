using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MJC.CoreAPI.Template.WebAPI.Data.Entities;
using MJC.CoreAPI.Template.WebAPI.Data.Repositories;
using MJC.CoreAPI.Template.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MJC.CoreAPI.Template.WebAPI.Controllers
{
    [Route("api/dummies")]
    [ValidateModel]
    [ApiVersion("1.0")]
    public class DummyController : Controller
    {
        private readonly IDummyApiRepository _repo;
        private ILogger<DummyController> _logger;
        private IMapper _mapper;

        public DummyController(IDummyApiRepository dummyApiRepository,
            ILogger<DummyController> logger,
            IMapper mapper)
        {
            _repo = dummyApiRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public ActionResult GetAll()
        {
            try
            {
                IEnumerable<Dummy> dummies = null;

                dummies = _repo.GetDummies();

                if (dummies != null && dummies.Count() > 0)
                {
                    var results = _mapper.Map<IEnumerable<DummyDto>>(dummies);
                    return Ok(results);
                }
                else
                {
                    return NotFound("No dummies found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return BadRequest();
        }

        [HttpGet("{dummyId}", Name = "DummyGet")]
        public ActionResult GetById(int dummyId)
        {
            try
            {
                var dummy = _repo.GetDummy(dummyId);

                if (dummy != null)
                {
                    var result = _mapper.Map<DummyDto>(dummy);
                    return Ok(result);
                }
                else
                {
                    return NotFound("Dummy was not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody]DummyDtoForCreation model)
        {
            try
            {
                _logger.LogInformation("Creating a new dummy.");
                var dummy = _mapper.Map<Dummy>(model);

                if (!_repo.DummyExists(dummy.Name))
                {
                    _repo.Add(dummy);

                    if (_repo.Save())
                    {
                        var newUri = Url.Link("DummyGet", new { dummyId = dummy.Id });
                        return Created(newUri, _mapper.Map<DummyDto>(dummy));
                    }
                    else
                    {
                        _logger.LogWarning($"Could not save dummy to the database.");
                    }
                }
                else
                {
                    return BadRequest("Duplicate exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving the dummy: {ex}");
                return BadRequest(ex.Message);
            }

            return BadRequest("123");
        }


        [HttpPut("{dummyId}")]
        public IActionResult Put(int dummyId, [FromBody]DummyDtoForUpdate model)
        {
            try
            {
                var oldDummy = _repo.GetDummy(dummyId);

                if (oldDummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                _mapper.Map(model, oldDummy);

                if (_repo.Save())
                {
                    return Ok(_mapper.Map<DummyDtoForUpdate>(oldDummy));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }


        [HttpPatch("{dummyId}")]
        public IActionResult Patch(int dummyId, [FromBody]JsonPatchDocument<Dummy> patchedDummy)
        {
            try
            {
                var oldDummy = _repo.GetDummy(dummyId);

                if (oldDummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                if (patchedDummy == null)
                {
                    return BadRequest();
                }

                patchedDummy.ApplyTo(oldDummy);

                if (_repo.Save())
                {
                    return Ok(_mapper.Map<DummyDtoForUpdate>(oldDummy));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }

        [HttpDelete("{dummyId}")]
        public IActionResult Delete(int dummyId)
        {
            try
            {
                var dummy = _repo.GetDummy(dummyId);

                if (dummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                _repo.Delete(dummy);

                if (_repo.Save())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }
    }
}
