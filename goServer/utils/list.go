package utils

func Remove(values []*interface{}, val *interface{}) []*interface{} {

	if len(values) <= 0 {
		return values
	}

	res := []*interface{}{}

	for i := 0; i < len(values); i++ {
		if values[i] == val {
			continue
		}
		v := values[i]
		res = append(res, v)
	}
	return res
}
