import { useState } from "react";
import { api } from "../services/api";
import ErrorBanner from "../components/ErrorBanner";

export default function ProductForm(){
  const [name,setName] = useState("");
  const [price,setPrice] = useState<number | "">("");
  const [stock,setStock] = useState<number | "">("");
  const [ok,setOk] = useState("");
  const [err,setErr] = useState("");

  async function submit(e: React.FormEvent) {
    e.preventDefault(); setOk(""); setErr("");

    if (!name.trim()) return setErr("Name es requerido.");
    if (price === "" || Number(price) <= 0) return setErr("Price debe ser > 0.");
    if (stock === "" || Number(stock) < 0) return setErr("Stock inválido.");

    try{
      const body = { name: name.trim(), price: Number(price), stock: Number(stock) };
      await api.post("/products", body);
      setOk("Creado.");
      setName(""); setPrice(""); setStock("");
    }catch(e:any){
      setErr(e?.response?.data?.detail || "No se pudo crear.");
    }
  }

  return (
    <section>
      <h2>Nuevo producto</h2>
      <ErrorBanner message={err} />
      {ok && <p className="success card">✅ {ok}</p>}

      <form className="card" onSubmit={submit}>
        <div className="row">
          <input placeholder="Name" value={name} onChange={e=>setName(e.target.value)} />
        </div>
        <div className="row">
          <input type="number" placeholder="Price" value={price} onChange={e=>setPrice(e.target.value === "" ? "" : Number(e.target.value))} />
          <input type="number" placeholder="Stock" value={stock} onChange={e=>setStock(e.target.value === "" ? "" : Number(e.target.value))} />
        </div>
        <button>Guardar</button>
      </form>
    </section>
  );
}
