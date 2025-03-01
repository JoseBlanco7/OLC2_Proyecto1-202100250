'use client';
import { Editor } from '@monaco-editor/react';
import { useState } from 'react';

const API_URL = 'http://localhost:5233';

export default function Home() {
  const [code, setCode] = useState<string>('');
  const [output, setOutput] = useState<string>('');
  const [error, setError] = useState<string>('');

  const handleExecute = async () => {
    try {
      const response = await fetch(`${API_URL}/compile`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ code }),
      });
      const data = await response.json();
      if (!response.ok) {
        throw new Error(data.error || 'Error desconocido');
      }
      setOutput(data.result);
      setError('');
    } catch (err) {
      setOutput(err instanceof Error ? err.message : 'Error desconocido');
    }
  };

  const handleCreateFile = () => {
    setCode('');
  };

  const handleOpenFile = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setCode(e.target?.result as string);
      };
      reader.readAsText(file);
    }
  };

  const handleSaveFile = () => {
    const blob = new Blob([code], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'archivo.glt';
    a.click();
    URL.revokeObjectURL(url);
  };

  return (
    <div className='flex flex-col items-center justify-center min-h-screen py-2 bg-gray-600'>
      <h1 className='text-3xl font-bold mb-4'>PROYECTO</h1>
      <div className='flex flex-row w-3/4 justify-center space-x-4'>
        <div className='flex flex-col w-1/2 bg-gray-400 p-4 rounded'>
          <h2 className='text-xl font-semibold mb-2 text-center'>ENTRADA</h2>
          <Editor
            height='60vh'
            defaultLanguage='javascript'
            theme='vs-dark'
            value={code}
            onChange={(value) => setCode(value || '')}
          />
        </div>
        <div className='flex flex-col w-1/2 bg-gray-400 p-4 rounded'>
          <h2 className='text-xl font-semibold mb-2 text-center'>SALIDA</h2>
          <Editor
            height='60vh'
            defaultLanguage='javascript'
            theme='vs-dark'
            value={output || error}
            options={{ readOnly: true }}
          />
        </div>
      </div>
      <div className='flex space-x-4 my-4'>
        <button
          className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded'
          onClick={handleExecute}
        >
          Ejecutar
        </button>
        <button
          className='bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded'
          onClick={handleCreateFile}
        >
          Crear Archivo
        </button>
        <input
          type='file'
          accept='.glt'
          onChange={handleOpenFile}
          className='hidden'
          id='fileInput'
        />
        <label
          htmlFor='fileInput'
          className='bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 rounded cursor-pointer'
        >
          Abrir Archivo
        </label>
        <button
          className='bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded'
          onClick={handleSaveFile}
        >
          Guardar Archivo
        </button>
      </div>
    </div>
  );
}