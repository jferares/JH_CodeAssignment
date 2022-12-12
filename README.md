# JH_CodeAssignment - Twitter Info API

- appsettings.json contains strings for ApiKey and ApiKeySecret.  Provide those and the API will use them.
./Requests/TwitterApi/ contains some http requests (within VSCode I use the REST Client extension, they're setup for that).  Primarily use CreateTwitterInfo.http to test the API itself.
- The UnitTest project uses xUnit and the tests there are aimed at testing the logic that calls Twitter for Tweets and then puts them into a list and finds the Top10 Hashtags.  Note that the UnitTests make use of mock_tweets.json

- [Twitter Info API](#twitter-info-api)
  - [Create Twitter Info](#create-twitter-info)
    - [Create Twitter Info Request](#create-twitter-info-request)
    - [Create Twitter Info Response](#create-twitter-info-response)
  - [Get Twitter Info](#get-twitter-info)
    - [Get Twitter Info Request](#get-twitter-info-request)
    - [Get Twitter Info Response](#get-twitter-info-response)
  - [Update Twitter Info](#update-twitter-info)
    - [Update Twitter Info Request](#update-twitter-info-request)
    - [Update Twitter Info Response](#update-twitter-info-response)
  - [Delete Twitter Info](#delete-twitter-info)
    - [Delete Twitter Info Request](#delete-twitter-info-request)
    - [Delete Twitter Info Response](#delete-twitter-info-response)

## Create Twitter Info

### Create Twitter Info Request

```js
POST /twitterinfos
```

```json
{
    "name": "Joe Twitter Info",
    "description": "Info pulled for JH code challenge",
    "requestedDateTime": "2022-12-07T08:00:00",
    "duration": "5"
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
    "requestedDateTime": "2022-12-07T08:00:00",
    "duration": "5",
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
    "requestedDateTime": "2022-12-07T08:00:00",
    "duration": "5",
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
    "requestedDateTime": "2022-12-07T08:00:00",
    "duration": "5",
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
