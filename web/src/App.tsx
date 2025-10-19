import { BrowserRouter,Routes, Route, NavLink } from "react-router-dom";
import ProductsList from "./routes/ProductsList";
import ProductForm from "./routes/ProductForm";

export default function App(){
  return(
     <BrowserRouter>
       <header className="bar">
        <h1>Products</h1>
        <nav className="nav">
          <NavLink to="/" end>Lista</NavLink>
          <NavLink to="/new">Nuevo</NavLink>
        </nav>
       </header>
       <main className="content">
        <Routes>
          <Route path="/" element={<ProductsList />} />
          <Route path="/new" element={<ProductsList />} />
        </Routes>
       </main>
     </BrowserRouter>    
  )
}


// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <div>
//         <a href="https://vite.dev" target="_blank">
//           <img src={viteLogo} className="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} className="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div className="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.tsx</code> and save to test HMR
//         </p>
//       </div>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
