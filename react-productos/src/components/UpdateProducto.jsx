import React from "react";
import {useState,useEffect} from 'react'

export default function UpdateProducto({datos}) {
 const [tipoProducto,setTipoProducto] = useState([]);
 const [showModal, setShowModal] = React.useState(false);
 const [producto,setProducto] = useState(
        {
            id: datos.id,
            idTipoProducto:'',
            nombre: '',
            precio: ''
        },{
            id: '',
            cantidad: ''
        }        
)

const handleChange =(e)=>{
    setProducto({...producto, [e.target.name]: e.target.value})
}

const handleSubmit = (e) =>{
    e.preventDefault();
      EditarProducto(producto);
      setShowModal(false)
    
}

  useEffect(() => {
    fetch('http://localhost:5266/api/TipoProducto/GetTipoProducto')
      .then((response) => {
        return response.json()
      })
      .then((tipoProducto) => {
        setTipoProducto(tipoProducto.msg) 
      })
  }, [])

  return (
    <>
     <button
        className="bg-amber-300 text-white active:bg-green-600 font-bold uppercase text-sm px-7 py-2 mx-1 h-9 rounded shadow hover:shadow-lg outline-none focus:outline-none ease-linear transition-all duration-150"
        type="button"
        onClick={() => setShowModal(true)}
      >
        Editar
      </button>
      {showModal ? (
        <>
          <div
            className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
          >
            <div className="relative w-auto my-6 mx-auto max-w-3xl">
              <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                  <h3 className="text-2xl font-semibold">Editar producto</h3>
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
                  <input type="text" className="p-2 m-2 border-b-2 border-amber-300" name={'nombre'} placeholder={datos.nombre} required onChange={handleChange}/>
                  <input type="text" name={'cantidad'}  className="p-2 m-2 border-b-2 border-amber-300" placeholder={datos.cantidad} required  onChange={handleChange}/>
                  <div>
                  <input type="text" name={'precio'}  className="p-2 m-2 border-b-2 border-amber-300" placeholder={datos.precio} required  onChange={handleChange}/>

                  <select name={'idTipoProducto'}  className="p-2 m-2 border-b-2 border-amber-300" style={{width:'13.5rem'}} required onChange={handleChange}>
                  {tipoProducto.map(tipoProducto => {
                    return (
                            <option key={tipoProducto.id} value={tipoProducto.id}>{tipoProducto.descripcion}</option>
                    );
                    })}
                    </select>

                    </div>
                    <div className="mt-9 flex justify-end">
                    <button
                    className="text-black-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                    type="button"
                    onClick={() => setShowModal(false)}
                  >
                    Cancelar
                  </button>
                  <button
                    className="bg-amber-300 text-white active:bg-amber-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
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

const EditarProducto = async (producto) =>{
    const req = await fetch("http://localhost:5266/api/Producto/PutProductos",{
           method:'PUT',
           headers:{
               "content-type":"application/json"
           },
           body: JSON.stringify(
            {
            "producto": {
                id: producto.id,
               nombre: producto.nombre,
               precio: producto.precio,
               idTipoProducto: producto.idTipoProducto
           }, 
           "stock": {
              id: producto.id,
              cantidad: producto.cantidad
           }})
       })   
       
      location.reload();
   }