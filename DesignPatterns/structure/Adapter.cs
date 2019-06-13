using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structure
{
    #region Service sample

    class ComplexService
    {
        public bool CurrentState { get; set; }
        public void DoSomethingOne()
        {

        }
        public void DoSomethingTwo()
        {

        }
        public void DoSomethingThree()
        {

        }
    }

    class ServiceWrapper
    {
        private readonly ComplexService _service;

        public ServiceWrapper(ComplexService service)
        {
            _service = service;
        }

        public void DoSomethingSimple()
        {
            _service.DoSomethingOne();
            _service.DoSomethingTwo();
            _service.DoSomethingThree();
        }
    }

    #endregion

    #region Model sample

    public class DomainModel
    {
        public string PropOne { get; set; }
        public string PropTwo { get; set; }
        public int PropThree { get; set; }
    }

    public class DomainModelListInfo
    {
        private readonly DomainModel _model;

        public DomainModelListInfo(DomainModel model)
        {
            _model = model;
        }

        public string ComposedProp => _model.PropOne + " " + _model.PropTwo + " = " + _model.PropThree;
    }

    #endregion

}
