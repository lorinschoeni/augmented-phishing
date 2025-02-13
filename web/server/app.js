
const express = require('express')
const cors = require('cors');
const app = express()
const port = 3000
const path = require('path')
const fs = require('fs')

const datasetPath = `${path.join(__dirname, 'data')}/dataset.json`

app.use(cors())
app.options('*', cors())
app.use(express.json({ limit: '256mb' }))

app.get('/api/active/config', (req, res) => {

	fs.readFile(`${path.join(__dirname, 'data')}/__active.json`, (err, json) => {
		if (err) {
			res.json({ active: null })
			return false
		}

    const { active } = JSON.parse(json)
    res.json({ active })
  })

})

app.post('/api/active/slide', (req, res) => {

	fs.readFile(`${path.join(__dirname, 'data')}/__active.json`, (err, json) => {
    if (err) {
    	res.status(400).send('Please create a demo setup first and mark it as active')
    	return false
    }

    let activeJson = JSON.parse(json)
		activeJson.slide = req.body.slide || 0

		fs.writeFile(`${path.join(__dirname, 'data')}/__active.json`, JSON.stringify(activeJson), err => {
			res.status(200).send('Updated')
	  })

  })

})

app.post('/api/active/config', (req, res) => {

	if (!req.body || !req.body.filename) {
		console.log('No file specified')
		res.status(400).send()
		return false
	}

	const data = { 
		active: req.body.filename,
		slide: 0
	}

	fs.writeFile(`${path.join(__dirname, 'data')}/__active.json`, JSON.stringify(data), err => {
    console.log('Error occured')
  })

	res.status(200).send('Updated')

})

app.get('/api/config/:file', (req, res) => {

	if (!req.params || !req.params.file) {
		console.log('No file specified')
		return false
	}

	try {

		fs.readFile(`${path.join(__dirname, 'data')}/${req.params.file}.json`, (err, json) => {
      res.json(JSON.parse(json))
	  })	

	} catch (e) {
		
		console.log('Error reading file')
		return false

	}

	
})

app.get('/api/config', (req, res) => {

	fs.readdir(path.join(__dirname, 'data'), (err, files) => {
    
    if (err) {
			console.log('Unable to scan dir')
			return false
    } 
  	
  	res.json({ files: files.map(x => x.replace('.json', '')).filter(x => !['__active', '.DS_Store'].includes(x)) })

  })

})

app.post('/api/update', (req, res) => {
  if (req.body && (!req.body.data || !req.body.filename)) {
  	res.status(404).send('No body')
  	return false
  }
  
  let filename = req.body.filename.replace('.json', '').replace(/ /g, "_").replace(/[^a-z0-9]/gi, '_').toLowerCase()

  fs.writeFile(`${path.join(__dirname, 'data')}/${filename}.json`, JSON.stringify(req.body.data), err => {
    if (err) console.log('Error writing file')
  })

  res.status(200).send('Updated')
})

app.post('/api/new', (req, res) => {
  if (req.body && (!req.body.filename || !req.body.data)) {
  	res.status(404).send('No filename')
  	return false
  }
  
  const filename = req.body.filename.replace('.json', '').replace(/ /g, "_").replace(/[^a-z0-9]/gi, '_').toLowerCase()

  fs.writeFile(`${path.join(__dirname, 'data')}/${filename}.json`, JSON.stringify(req.body.data), err => {
    if (err) console.log('Error writing file')
  })

  res.status(201).send({ filename })
})

app.get('/api/headset', (req, res) => {

	fs.readFile(`${path.join(__dirname, 'data')}/__active.json`, (err, json) => {
    if (err) {
    	res.status(400).send('Please create a demo setup first and mark it as active')
    	return false
    }
    
    const { active, slide } = JSON.parse(json)
    fs.readFile(`${path.join(__dirname, 'data')}/${active}.json`, (err, setup) => {
    	console.log(active, slide)
    	res.json(JSON.parse(setup)[slide])
  	})

  })

})

app.get('/api/web', (req, res) => {

	fs.readFile(`${path.join(__dirname, 'data')}/__active.json`, (err, json) => {
    if (err) {
    	res.status(400).send('Please create a demo setup first and mark it as active')
    	return false
    }
    
    const { active } = JSON.parse(json)
    fs.readFile(`${path.join(__dirname, 'data')}/${active}.json`, (err, setup) => {
    	res.json(JSON.parse(setup))
  	})

  })

})

app.get('/', (req, res) => {
	res.sendFile(path.join(path.join(__dirname, 'client_dist/'), 'index.html'))
})

app.use('/js', express.static(path.join(__dirname, 'client_dist/js')))
app.use('/css', express.static(path.join(__dirname, 'client_dist/css')))
app.use('/img', express.static(path.join(__dirname, 'client_dist/img')))
app.use('/icons', express.static(path.join(__dirname, 'client_dist/icons')))
app.use('/image', express.static(path.join(__dirname, 'client_dist/image')))
app.use('/audio', express.static(path.join(__dirname, 'client_dist/audio')))
app.use('/video', express.static(path.join(__dirname, 'client_dist/video')))

app.listen(port, () => {
	console.log(`ETH testbed server is running`)
})