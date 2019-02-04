import { Rate } from "./model";

export async function calcRate(entry: Date, exit: Date) {
    const resp = await fetch('api/rate/', {
        method: 'POST',
        body: JSON.stringify({ entry, exit }),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    return await resp.json() as Rate
}
