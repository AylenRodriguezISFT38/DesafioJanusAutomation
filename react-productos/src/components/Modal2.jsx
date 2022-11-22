import React from "react";
import {useState} from 'react'

export default function Modal2() {
 const [showModal, setShowModal] = React.useState(false);

 const [tipoProducto,setTipoProducto] = useState({descripcion:''})

const handleChange =(e)=>{
    setTipoProducto({...tipoProducto, [e.target.name]: e.target.value})
}

const handleSubmit = (e) =>{
    e.preventDefault();
  
      CrearTipoProducto(tipoProducto);
      setShowModal(false)
}

  return (
    <>
      <button
        className="bg-emerald-500 text-white active:bg-green-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mt-10 ease-linear transition-all duration-150"
        type="button"
        onClick={() => setShowModal(true)}
      >
        crear
      </button>
      {showModal ? (
        <>
          <div
            className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
          >
            <div className="relative w-auto my-6 mx-auto max-w-3xl">
              <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                  <h3 className="text-2xl font-semibold">Crear tipo de producto</h3>
                  <button
                    className="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                    onClick={() => setShowModal(false)}
                  >
                    <span className="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">
                      ×
                    </span>
                  </button>
                </div>
                
                <div className="relative p-6 ">
                <form onSubmit={handleSubmit}>
                  <input type="text" className="p-2 m-2 border-b-2 border-emerald-500" name={'descripcion'} required placeholder="Tipo de producto" onChange={handleChange}/>
                  
                    <div className="mt-9 flex justify-end">
                    <button
                    className="text-black-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                    type="button"
                    onClick={() => setShowModal(false)}
                  >
                    Cancelar
                  </button>
                  <button
                    className="bg-emerald-500 text-white active:bg-emerald-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                    type="submit"
                  >
                    Guardar
                  </button>
                  </div>
                  </form>
                </div>
             
              </div>
            </div>
          </div>
          <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
      ) : null}
    </>
  );
}

const CrearTipoProducto = async (tipoProducto) =>{

    const req = await fetch("http://localhost:5266/api/TipoProducto/PostTipoProducto",{
           method:'POST',
           headers:{
               "content-type":"application/json"
           },
           body: JSON.stringify({descripcion:tipoProducto.descripcion})
       })   
      location.reload();
   }