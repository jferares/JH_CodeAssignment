# Twitter Info API

- [Twitter Info API](#twitter-info-api)
  - [Get Twitter Info](#get-twitter-info)
    - [Get Twitter Info Request](#get-twitter-info-request)
    - [Get Twitter Info Response](#get-twitter-info-response)

## Create Twitter Info

### Create Twitter Info Request

```js
POST /twitterinfos
```

```json
{
    "name": "Joe Twitter Info",
    "description": "Info pulled for JH code challenge",
    "startDateTime": "2022-12-07T08:00:00",
    "endDateTime": "2022-12-07T11:00:00"
}
```

### Create Twitter Info Response

```js
201 Created
```

```yml
Location: {{host}}/twitterinfos/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Joe Twitter Info",
    "description": "Info pulled for JH code challenge",
    "startDateTime": "2022-12-07T08:00:00",
    "endDateTime": "2022-12-07T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "tweets": [
        "content",
        "content",
        "content",
        "content"
    ],
    "top10": [
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content"
    ]
}
```

## Get Twitter Info

### Get Twitter Info Request

```js
GET /twitterinfos/{{id}}
```

### Get Twitter Info Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Joe Twitter Info",
    "description": "Info pulled for JH code challenge",
    "startDateTime": "2022-12-07T08:00:00",
    "endDateTime": "2022-12-07T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "tweets": [
        "content",
        "content",
        "content",
        "content"
    ],
    "top10": [
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content"
    ]
}
```

## Update Twitter Info

### Update Twitter Info Request

```js
PUT /twitterinfos/{{id}}
```

```json
{
    "name": "Joe Twitter Info",
    "description": "Info pulled for JH code challenge",
    "startDateTime": "2022-12-07T08:00:00",
    "endDateTime": "2022-12-07T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "tweets": [
        "content",
        "content",
        "content",
        "content"
    ],
    "top10": [
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content",
        "content"
    ]
}
```

### Update Twitter Info Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/twitterinfos/{{id}}
```

## Delete Twitter Info

### Delete Twitter Info Request

```js
DELETE /twitterinfos/{{id}}
```

### Delete Twitter Info Response

```js
204 No Content
```