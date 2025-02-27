'use client';
import { Editor } from '@monaco-editor/react';
import { useState } from 'react';

const API_URL = 'http://localhost:5233';

export default function Home() {
  const [code, setCode] = useState<string>('');
  const [output, setOutput] = useState<string>('');

  const handleExecute = () => {
    fetch(`${API_URL}/compile`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ code }),
    })
      .then((response) => response.json())
      .then((data) => {
        setOutput(data.result);
      })
      .catch((error) => {
        console.error('Error:', error);
      });
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
            value={output}
            options={{ readOnly: true }}
          />
        </div>
      </div>
      <button
        className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-4'
        onClick={handleExecute}
      >
        Ejecutar
      </button>
    </div>
  );
}