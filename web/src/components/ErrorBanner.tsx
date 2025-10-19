// export default function ErrorBanner({ message }: { message: string }) {
//   if (!message) return null;
//   return <p className="error card">⚠️ {message}</p>;
// }

export default function ErrorBanner({ message } : {message : string}){
    if(!message) return null;
    return <p className="error card">⚠️ {message}</p>;
}